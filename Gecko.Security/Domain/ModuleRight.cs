using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	ģ��Ȩ�ޡ�
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
        /// Ȩ�ޱ�ʾ��
        /// </summary>
        public virtual string RightTag
        {
            get { return _right_tag; }
            set { _right_tag = value; }
        }

        /// <summary>
        /// ģ�顣
        /// </summary>
        public virtual Module Module
        {
            get { return _module; }
            set { _module = value; }
        }

        /// <summary>
        /// �Ե�ǰȨ���п϶���Ȩ�Ľ�ɫ��
        /// </summary>
        public virtual ISet<Role> RolesGrant
        {
            get { return _roles_grant; }
            set { _roles_grant = value; }
        }

        /// <summary>
        /// �Ե�ǰȨ���з���Ȩ�Ľ�ɫ��
        /// </summary>
        public virtual ISet<Role> RolesDeny
        {
            get { return _roles_deny; }
            set { _roles_deny = value; }
        }

        /// <summary>
        /// �Ե�ǰȨ���п϶���Ȩ��ְԱ��
        /// </summary>
        public virtual ISet<Staff> StaffGrant
        {
            get { return _staff_grant; }
            set { _staff_grant = value; }
        }

        /// <summary>
        /// �Ե�ǰȨ���з���Ȩ��ְԱ��
        /// </summary>
        public virtual ISet<Staff> StaffDeny
        {
            get { return _staff_deny; }
            set { _staff_deny = value; }
        }

        #endregion

        #region ���캯��

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

        #region ��������


        /// <summary>
        /// ɾ���뵱ǰģ��Ȩ���йص�������Ȩ��
        /// </summary>
        public virtual void RemoveAllPermissions()
        {
            //ɾ����ɫ�϶���Ȩ��
            foreach (Role role in this.RolesGrant)
            {
                role.ModuleRightsGrant.Remove(this);
            }
            this.RolesGrant.Clear();

            //ɾ����ɫ����Ȩ��
            foreach (Role role in this.RolesDeny)
            {
                role.ModuleRightsDeny.Remove(this);
            }
            this.RolesDeny.Clear();

            //ɾ��ְԱ�϶���Ȩ��
            foreach (Staff staff in this.StaffGrant)
            {
                staff.ModuleRightsGrant.Remove(this);
            }
            this.StaffGrant.Clear();

            //ɾ��ְԱ����Ȩ��
            foreach (Staff staff in this.StaffDeny)
            {
                staff.ModuleRightsDeny.Remove(this);
            }
            this.StaffDeny.Clear();
        }


        /// <summary>
        /// ������ģ�����롣
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
