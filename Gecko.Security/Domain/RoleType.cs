using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	��ɫ���ࡣ
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
        /// ����ɫ���ࡣ
        /// </summary>
        public virtual RoleType ParentRoleType
        {
            get { return _parent_role_type; }
            set { _parent_role_type = value; }
        }

        /// <summary>
        /// ��ɫ�б�
        /// </summary>
        public virtual IList Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        /// <summary>
        /// �ӽ�ɫ�����б�
        /// </summary>
        public virtual IList SubRoleTypes
        {
            get { return _sub_role_types; }
            set { _sub_role_types = value; }
        }

        #endregion

        #region ���캯��

        public RoleType()
        {
            _id = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _parent_role_type = null;
            _roles = new List<Role>();
            _sub_role_types = new List<RoleType>();
        }

        #endregion

        #region ��������


        /// <summary>
        /// �ƶ���ɫ���ࡣ
        /// </summary>
        /// <param name="newParent">�µĸ���ɫ���ࡣ</param>
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
        /// ���ϼ���ɫ�������롣��ʹ�Լ���Ϊ����Ľ�ɫ���ࣩ
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
        /// ����һ���ӽ�ɫ���ࡣ
        /// </summary>
        /// <param name="subDepartment">�ӽ�ɫ���ࡣ</param>
        public virtual void AddSubRoleType(RoleType subRoleType)
        {
            this.SubRoleTypes.Add(subRoleType);
            subRoleType.ParentRoleType = this;
        }


        /// <summary>
        /// ����һ����ɫ��
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
