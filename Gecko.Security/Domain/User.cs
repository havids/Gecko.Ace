using System;
using System.Collections;

namespace Gecko.Security.Domain
{
	/// <summary>
	///	前台用户。
	/// </summary>
	public class User
	{
		private string _login_id;
		private string _password;
		private string _name;
        private int _sex;
        private DateTime? _birthday;
		private string _id_card;
		private string _office_phone;
		private string _family_phone;
		private string _cell_phone;
		private string _email;
		private string _address;
		private string _zip_code;
		private string _remark;
        private int _disabled;
        private DateTime _register_date;

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
		/// 出生日期。
		/// </summary>
        public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
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
		/// 办公室电话。
		/// </summary>
        public virtual string OfficePhone
		{
			get { return _office_phone; }
			set { _office_phone = value; }
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
		/// 通讯地址。
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
		/// 已禁用。
		/// </summary>
        public virtual int Disabled
		{
			get { return _disabled; }
			set { _disabled = value; }
        }

        /// <summary>
        /// 注册时间。
        /// </summary>
        public virtual DateTime RegisterDate
        {
            get { return _register_date; }
            set { _register_date = value; }
        }
				
		#endregion 

        #region 构造函数

        public User()
		{
			_login_id = String.Empty;
			_password = String.Empty; 
			_name = String.Empty;
            _sex = 0;
			//_birthday = null;
			_id_card = String.Empty;
			_office_phone = String.Empty; 
			_family_phone = String.Empty;
			_cell_phone = String.Empty; 
			_email = String.Empty; 
			_address = String.Empty; 
			_zip_code = String.Empty; 
			_remark = String.Empty; 
			_disabled = 0;
		}

		#endregion

    }
}
