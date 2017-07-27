using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
	/// <summary>
	///	职员。
	/// </summary>
	public class Staff
	{
		private string _login_id;
		private string _password;
		private string _code;
		private string _name;
        private int _sex;
        private int _married;
		private string _id_card;
        private string _country_tag;
		private string _nation_tag;
		private string _position_tag;
		private string _title_tag;
		private string _political_appearance_tag;
		private string _degree_tag;
        private DateTime? _birthday;
        private DateTime? _enters_day;
        private DateTime? _leaves_day;
		private string _office_phone;
		private string _ext_number;
		private string _family_phone;
		private string _cell_phone;
		private string _email;
		private string _address;
		private string _zip_code;
		private string _remark;
		private int _is_inner_user;
        private int _disabled;
		private int _order_id;
        private Department _department;
        private ISet<Role> _roles;
        private ISet<ModuleRight> _module_rights_grant;
        private ISet<ModuleRight> _module_rights_deny;

        #region 属性

        /// <summary>
        /// 登录ID。
		/// </summary>
        public virtual string LoginId
		{
			get { return _login_id; }
			set { _login_id = value; }
		}

		/// <summary>
		/// 登录密码。
		/// </summary>
        public virtual string Password
		{
			get { return _password; }
			set {  _password = value; }
		}
			
		/// <summary>
		/// 编号。
		/// </summary>
        public virtual string Code
		{
			get { return _code; }
			set { _code = value; }
		}
			
		/// <summary>
		/// 姓名。
		/// </summary>
        public virtual string Name
		{
			get { return _name; }
			set { _name = value; }
		}
			
		/// <summary>
		/// 性别。
		/// </summary>
        public virtual int Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}

		/// <summary>
		/// 婚否。
		/// </summary>
        public virtual int Married
		{
			get { return _married; }
			set { _married = value; }
		}
			
		/// <summary>
		/// 身份证号码。
		/// </summary>
        public virtual string IdCard
		{
			get { return _id_card; }
			set { _id_card = value; }
		}
			
		/// <summary>
		/// 国籍。
		/// </summary>
        public virtual string CountryTag
		{
            get { return _country_tag; }
            set { _country_tag = value; }
		}
			
		/// <summary>
		/// 民族。
		/// </summary>
        public virtual string NationTag
		{
			get { return _nation_tag; }
			set { _nation_tag = value; }
		}
			
		/// <summary>
		/// 职位。
		/// </summary>
        public virtual string PositionTag
		{
			get { return _position_tag; }
			set { _position_tag = value; }
		}
			
		/// <summary>
		/// 职称。
		/// </summary>
        public virtual string TitleTag
		{
			get { return _title_tag; }
			set	{ _title_tag = value; }
		}
			
		/// <summary>
		/// 政治面貌。
		/// </summary>
        public virtual string PoliticalAppearanceTag
		{
			get { return _political_appearance_tag; }
			set { _political_appearance_tag = value; }
		}
			
		/// <summary>
		/// 最高学历。
		/// </summary>
        public virtual string DegreeTag
		{
			get { return _degree_tag; }
			set	{ _degree_tag = value; }
		}
			
		/// <summary>
		/// 出生日期。
		/// </summary>
        public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}
			
		/// <summary>
		/// 入职日期。
		/// </summary>
        public virtual DateTime? EntersDay
		{
			get { return _enters_day; }
			set { _enters_day = value; }
		}
			
		/// <summary>
		/// 离职日期。
		/// </summary>
        public virtual DateTime? LeavesDay
		{
			get { return _leaves_day; }
			set { _leaves_day = value; }
		}
			
		/// <summary>
		/// 办公室电话。
		/// </summary>
        public virtual string OfficePhone
		{
			get { return _office_phone; }
			set { _office_phone = value; }
		}
			
		/// <summary>
		/// 分机号码。
		/// </summary>
        public virtual string ExtNumber
		{
			get { return _ext_number; }
			set { _ext_number = value; }
		}
			
		/// <summary>
		/// 家庭电话。
		/// </summary>
        public virtual string FamilyPhone
		{
			get { return _family_phone; }
			set { _family_phone = value; }
		}
			
		/// <summary>
		/// 手机。
		/// </summary>
        public virtual string CellPhone
		{
			get { return _cell_phone; }
			set { _cell_phone = value; }
		}
			
		/// <summary>
		/// Email。
		/// </summary>
        public virtual string Email
		{
			get { return _email; }
			set { _email = value; }
		}
			
		/// <summary>
		/// 家庭住址。
		/// </summary>
        public virtual string Address
		{
			get { return _address; }
			set { _address = value; }
		}
			
		/// <summary>
		/// 邮编。
		/// </summary>
        public virtual string ZipCode
		{
			get { return _zip_code; }
			set { _zip_code = value; }
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
		/// 是否内置用户。
		/// </summary>
        public virtual int IsInnerUser
		{
			get { return _is_inner_user; }
			set { _is_inner_user = value; }
		}
			
		/// <summary>
		/// 已禁用。
		/// </summary>
        public virtual int Disabled
		{
			get { return _disabled; }
			set { _disabled = value; }
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
        /// 所属部门。
        /// </summary>
        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }

        /// <summary>
        /// 角色列表。
        /// </summary>
        public virtual ISet<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        /// <summary>
        /// 对当前职员进行了肯定授权的模块权限。
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsGrant
        {
            get { return _module_rights_grant; }
            set { _module_rights_grant = value; }
        }

        /// <summary>
        /// 对当前职员进行了否定授权的模块权限。
        /// </summary>
        public virtual ISet<ModuleRight> ModuleRightsDeny
        {
            get { return _module_rights_deny; }
            set { _module_rights_deny = value; }
        }
				
		#endregion 

        #region 构造函数

        public Staff()
		{
			_login_id = String.Empty;
			_password = String.Empty; 
			_code = String.Empty; 
			_name = String.Empty;
            //_sex = 0;
            //_married = 0;
			_id_card = String.Empty;
            _country_tag = String.Empty; 
			_nation_tag = String.Empty; 
			_position_tag = String.Empty; 
			_title_tag = String.Empty; 
			_political_appearance_tag = String.Empty; 
			_degree_tag = String.Empty; 
			//_birthday = null;
            //_enters_day = null;
            //_leaves_day = null;
			_office_phone = String.Empty; 
			_ext_number = String.Empty; 
			_family_phone = String.Empty;
			_cell_phone = String.Empty; 
			_email = String.Empty; 
			_address = String.Empty; 
			_zip_code = String.Empty; 
			_remark = String.Empty; 
			_is_inner_user = 0; 
			_disabled = 0;
			_order_id = 0;
			_department = null;
            _roles = new HashSet<Role>();
            _module_rights_grant = new HashSet<ModuleRight>();
            _module_rights_deny = new HashSet<ModuleRight>();
		}

		#endregion

        #region 公共方法

        /// <summary>
        /// 移动职员。
        /// </summary>
        /// <param name="newParent">新的部门。</param>
        public virtual void MoveTo(Department newParent)
        {
            Department oldParent = this.Department;
            oldParent.Staff.Remove(this);

            newParent.Staff.Add(this);
            this.Department = newParent;
        }

        /// <summary>
        /// 删除对当前职员所有的模块授权。
        /// </summary>
        public virtual void RemoveAllPermissions()
        {
            //删除模块肯定授权。
            foreach(ModuleRight grant in this.ModuleRightsGrant)
            {
                grant.StaffGrant.Remove(this);
            }
            this.ModuleRightsGrant.Clear();

            //删除模块否定授权。
            foreach(ModuleRight deny in this.ModuleRightsDeny)
            {
                deny.StaffDeny.Remove(this);
            }
            this.ModuleRightsDeny.Clear();
        }


        /// <summary>
        /// 与所有的角色脱离关联。
        /// </summary>
        public virtual void BreakAwayFromRoles()
        {
            foreach(Role role in this.Roles)
            {
                role.Staff.Remove(this);
            }
            this.Roles.Clear();
        }


        /// <summary>
        /// 从所属部门脱离。
        /// </summary>
        public virtual void BreakAwayFromDepartment()
        {
            Department d = this.Department;
            this.Department = null;
            d.Staff.Remove(this);
        }


        /// <summary>
        /// 判断职员是否对某一项模块权限拥有肯定授权。
        /// </summary>
        /// <remarks>
        /// 授权判断顺序如下：
        /// （1）职员是否为内置用户。如果是则返回true，否则执行下一步。
        /// （2）职员本身是否对此权限做了否定授权。如果是则返回false，否则执行下一步。
        /// （3）职员本身是否对此权限做了肯定授权。如果是则返回true，否则执行下一步。
        /// （4）职员拥有的所有角色的集合中，是否有任何一个角色对此权限做了否定授权。如果是则返回false，否则执行下一步。
        /// （5）职员拥有的所有角色的集合中，是否有任何一个角色对此权限做了肯定授权。如果是则返回true，否则执行下一步。
        /// （6）返回false。（即职员不是内置用户，并且职员本身以及职员拥有的所有角色都没有提供对此权限的任何授权信息。）
        /// </remarks>
        /// <param name="moduleRight">模块权限。</param>
        /// <returns>是否有肯定授权。</returns>
        public virtual bool HasGrantPermission(ModuleRight moduleRight)
        {
            if(this.IsInnerUser == 1) return true;

            if (this.ModuleRightsDeny.Contains(moduleRight)) return false;
            if (this.ModuleRightsGrant.Contains(moduleRight)) return true;

            bool hasRoleGrant = false;
            foreach (Role role in this.Roles)
            {
                if (role.ModuleRightsDeny.Contains(moduleRight)) return false;
                if (role.ModuleRightsGrant.Contains(moduleRight)) hasRoleGrant = true;
            }

            return hasRoleGrant;
        }


        /// <summary>
        /// 获取对于某模块的所有肯定授权的权限标示。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <returns>权限标示集合。</returns>
        public virtual ArrayList GetGrantPermissions(Module module)
        {
            ArrayList alPermissions = new ArrayList();

            foreach (var k in module.ModuleRights)
            {
                ModuleRight mr = k.Value;
                if (this.HasGrantPermission(mr))
                {
                    alPermissions.Add(mr.RightTag);
                }
            }

            return alPermissions;
        }

        /// <summary>
        /// 获取当前登录用户可操作的菜单项
        /// </summary>
        /// <returns></returns>
        public virtual string GetSecurityMenu()
        {
            string items = string.Empty;

            IList modellist = Gecko.Security.Service.ModuleSrv.GetAllDisabledModule();
            if (modellist != null)
            {
                foreach (Module module in modellist)
                {
                    ModuleRight mr = Gecko.Security.Service.ModuleRightSrv.GetModuleRight(module, "rights_browse");
                    if (mr != null)
                    {
                        if (HasGrantPermission(mr))
                            items += module.ModuleUrl + ",";
                    }
                }
            }

            return items;
        }

        #endregion
    }
}
