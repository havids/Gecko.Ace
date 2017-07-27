using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	��ɫ��
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
        /// ��ɫ���ࡣ
        /// </summary>
        public virtual RoleType RoleType
        {
            get { return _role_type; }
            set { _role_type = value; }
        }

        /// <summary>
        /// ְԱ�б�
        /// </summary>
        public virtual ISet<Staff> Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        /// <summary>
        /// �Ե�ǰ��ɫ�����˿϶���Ȩ��ģ��Ȩ�ޡ�
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsGrant
        {
            get { return _module_rights_grant; }
            set { _module_rights_grant = value; }
        }

        /// <summary>
        /// �Ե�ǰ��ɫ�����˷���Ȩ��ģ��Ȩ�ޡ�
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsDeny
        {
            get { return _module_rights_deny; }
            set { _module_rights_deny = value; }
        }

        #endregion

        #region ���캯��

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

        #region ��������


        /// <summary>
        /// �ƶ���ɫ��
        /// </summary>
        /// <param name="newParent">�µĽ�ɫ���ࡣ</param>
        public virtual void MoveTo(RoleType newParent)
        {
            RoleType oldParent = this.RoleType;
            oldParent.Roles.Remove(this);

            newParent.Roles.Add(this);
            this.RoleType = newParent;
        }


        /// <summary>
        /// ɾ���Ե�ǰ��ɫ���е�ģ����Ȩ��
        /// </summary>
        public virtual void RemoveAllPermissions()
        {
            //ɾ��ģ��϶���Ȩ��
            foreach(ModuleRight grant in this.ModuleRightsGrant)
            {
                grant.RolesGrant.Remove(this);
            }
            this.ModuleRightsGrant.Clear();

            //ɾ��ģ�����Ȩ��
            foreach(ModuleRight deny in this.ModuleRightsDeny)
            {
                deny.RolesDeny.Remove(this);
            }
            this.ModuleRightsDeny.Clear();
        }


        /// <summary>
        /// �����е�ְԱ���������
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
        /// �ӽ�ɫ�������롣
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