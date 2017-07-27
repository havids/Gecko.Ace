using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	角色。
    /// </summary>
    public class Role
    {
        private string _id;
        private string _name;
        private string _remark;
        private int _order_id;
        private RoleType _role_type;
        private ISet<Staff> _staff;
        private ISet<ModuleRight> _module_rights_grant;
        private ISet<ModuleRight> _module_rights_deny;

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
        /// 角色分类。
        /// </summary>
        public virtual RoleType RoleType
        {
            get { return _role_type; }
            set { _role_type = value; }
        }

        /// <summary>
        /// 职员列表。
        /// </summary>
        public virtual ISet<Staff> Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        /// <summary>
        /// 对当前角色进行了肯定授权的模块权限。
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsGrant
        {
            get { return _module_rights_grant; }
            set { _module_rights_grant = value; }
        }

        /// <summary>
        /// 对当前角色进行了否定授权的模块权限。
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsDeny
        {
            get { return _module_rights_deny; }
            set { _module_rights_deny = value; }
        }

        #endregion

        #region 构造函数

        public Role()
        {
            _id = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _role_type = null;
            _staff = new HashSet<Staff>();
            _module_rights_grant = new HashSet<ModuleRight>();
            _module_rights_deny = new HashSet<ModuleRight>();
        }

        #endregion

        #region 公共方法


        /// <summary>
        /// 移动角色。
        /// </summary>
        /// <param name="newParent">新的角色分类。</param>
        public virtual void MoveTo(RoleType newParent)
        {
            RoleType oldParent = this.RoleType;
            oldParent.Roles.Remove(this);

            newParent.Roles.Add(this);
            this.RoleType = newParent;
        }


        /// <summary>
        /// 删除对当前角色所有的模块授权。
        /// </summary>
        public virtual void RemoveAllPermissions()
        {
            //删除模块肯定授权。
            foreach(ModuleRight grant in this.ModuleRightsGrant)
            {
                grant.RolesGrant.Remove(this);
            }
            this.ModuleRightsGrant.Clear();

            //删除模块否定授权。
            foreach(ModuleRight deny in this.ModuleRightsDeny)
            {
                deny.RolesDeny.Remove(this);
            }
            this.ModuleRightsDeny.Clear();
        }


        /// <summary>
        /// 与所有的职员脱离关联。
        /// </summary>
        public virtual void BreakAwayFromStaff()
        {
            foreach(Staff staff in this.Staff)
            {
                staff.Roles.Remove(this);
            }
            this.Staff.Clear();
        }


        /// <summary>
        /// 从角色分类脱离。
        /// </summary>
        public virtual void BreakAwayFromRoleType()
        {
            RoleType rt = this.RoleType;
            this.RoleType = null;
            rt.Roles.Remove(this);
        }


        #endregion

    }
}