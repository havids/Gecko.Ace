using System;
using System.Collections;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	角色分类。
    /// </summary>
    public class RoleType
    {
        private string _id;
        private string _name;
        private string _remark;
        private int _order_id;
        private RoleType _parent_role_type;
        private IList _roles;
        private IList _sub_role_types;

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
        /// 父角色分类。
        /// </summary>
        public virtual RoleType ParentRoleType
        {
            get { return _parent_role_type; }
            set { _parent_role_type = value; }
        }

        /// <summary>
        /// 角色列表。
        /// </summary>
        public virtual IList Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        /// <summary>
        /// 子角色分类列表。
        /// </summary>
        public virtual IList SubRoleTypes
        {
            get { return _sub_role_types; }
            set { _sub_role_types = value; }
        }

        #endregion

        #region 构造函数

        public RoleType()
        {
            _id = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _parent_role_type = null;
            _roles = new ArrayList();
            _sub_role_types = new ArrayList();
        }

        #endregion

        #region 公共方法


        /// <summary>
        /// 移动角色分类。
        /// </summary>
        /// <param name="newParent">新的父角色分类。</param>
        public virtual void MoveTo(RoleType newParent)
        {
            if(this.ParentRoleType != null)
            {
                RoleType oldParent = this.ParentRoleType;
                oldParent.SubRoleTypes.Remove(this);
            }

            if(newParent != null)
            {
                newParent.SubRoleTypes.Add(this);
                this.ParentRoleType = newParent;
            }
            else
            {
                this.ParentRoleType = null;
            }
        }


        /// <summary>
        /// 从上级角色分类脱离。（使自己成为顶层的角色分类）
        /// </summary>
        public virtual void BreakAwayFromParent()
        {
            if(this.ParentRoleType != null)
            {
                RoleType p = this.ParentRoleType;
                this.ParentRoleType = null;
                p.SubRoleTypes.Remove(this);
            }
        }


        /// <summary>
        /// 增加一个子角色分类。
        /// </summary>
        /// <param name="subDepartment">子角色分类。</param>
        public virtual void AddSubRoleType(RoleType subRoleType)
        {
            this.SubRoleTypes.Add(subRoleType);
            subRoleType.ParentRoleType = this;
        }


        /// <summary>
        /// 增加一个角色。
        /// </summary>
        /// <param name="role"></param>
        public virtual void AddRole(Role role)
        {
            this.Roles.Add(role);
            role.RoleType = this;
        }


        #endregion

    }
}
