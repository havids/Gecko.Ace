using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	模块权限。
    /// </summary>
    public class ModuleRight
    {
        private string _id;
        private string _right_tag;
        private Module _module;
        private ISet<Role> _roles_grant;
        private ISet<Role> _roles_deny;
        private ISet<Staff> _staff_grant;
        private ISet<Staff> _staff_deny;

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
        /// 权限标示。
        /// </summary>
        public virtual string RightTag
        {
            get { return _right_tag; }
            set { _right_tag = value; }
        }

        /// <summary>
        /// 模块。
        /// </summary>
        public virtual Module Module
        {
            get { return _module; }
            set { _module = value; }
        }

        /// <summary>
        /// 对当前权限有肯定授权的角色。
        /// </summary>
        public virtual ISet<Role> RolesGrant
        {
            get { return _roles_grant; }
            set { _roles_grant = value; }
        }

        /// <summary>
        /// 对当前权限有否定授权的角色。
        /// </summary>
        public virtual ISet<Role> RolesDeny
        {
            get { return _roles_deny; }
            set { _roles_deny = value; }
        }

        /// <summary>
        /// 对当前权限有肯定授权的职员。
        /// </summary>
        public virtual ISet<Staff> StaffGrant
        {
            get { return _staff_grant; }
            set { _staff_grant = value; }
        }

        /// <summary>
        /// 对当前权限有否定授权的职员。
        /// </summary>
        public virtual ISet<Staff> StaffDeny
        {
            get { return _staff_deny; }
            set { _staff_deny = value; }
        }

        #endregion

        #region 构造函数

        public ModuleRight()
        {
            _id = String.Empty;
            _right_tag = String.Empty;
            _module = null;
            _roles_grant = new HashSet<Role>();
            _roles_deny = new HashSet<Role>();
            _staff_grant = new HashSet<Staff>();
            _staff_deny = new HashSet<Staff>();
        }

        #endregion

        #region 公共方法


        /// <summary>
        /// 删除与当前模块权限有关的所有授权。
        /// </summary>
        public virtual void RemoveAllPermissions()
        {
            //删除角色肯定授权。
            foreach (Role role in this.RolesGrant)
            {
                role.ModuleRightsGrant.Remove(this);
            }
            this.RolesGrant.Clear();

            //删除角色否定授权。
            foreach (Role role in this.RolesDeny)
            {
                role.ModuleRightsDeny.Remove(this);
            }
            this.RolesDeny.Clear();

            //删除职员肯定授权。
            foreach (Staff staff in this.StaffGrant)
            {
                staff.ModuleRightsGrant.Remove(this);
            }
            this.StaffGrant.Clear();

            //删除职员否定授权。
            foreach (Staff staff in this.StaffDeny)
            {
                staff.ModuleRightsDeny.Remove(this);
            }
            this.StaffDeny.Clear();
        }


        /// <summary>
        /// 从所属模块脱离。
        /// </summary>
        public virtual void BreakAwayFromModule()
        {
            Module m = this.Module;
            this.Module = null;
            m.ModuleRights.Remove(this.RightTag);
        }


        #endregion

    }
}
