using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Gecko.Security.Domain
{
	/// <summary>
	///	部门。
	/// </summary>
    [Serializable]
    public class Department : ICloneable
	{
		private string _id;
		private string _name;
		private string _phone;
		private string _ext_number;
		private string _fax;
		private string _remark;
		private int _order_id;
        private Department _parent_department;
        private IList _staff;
        private IList _sub_departments;

        #region 属性

        /// <summary>
		/// ID。
		/// </summary>
        public virtual string Id
		{
			get { return _id; }
			set	{ _id = value; }
		}
			
		/// <summary>
		/// 名称。
		/// </summary>
        public virtual string Name
		{
			get { return _name; }
			set	{ _name = value; }
		}
			
		/// <summary>
		/// 电话。
		/// </summary>
        public virtual string Phone
		{
			get { return _phone; }
			set	{ _phone = value; }
		}
			
		/// <summary>
		/// 分机号码。
		/// </summary>
        public virtual string ExtNumber
		{
			get { return _ext_number; }
			set	{ _ext_number = value; }
		}
			
		/// <summary>
		/// 传真。
		/// </summary>
        public virtual string Fax
		{
			get { return _fax; }
			set	{ _fax = value; }
		}
			
		/// <summary>
		/// 备注。
		/// </summary>
        public virtual string Remark
		{
			get { return _remark; }
			set	{ _remark = value; }
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
        /// 上级部门。
        /// </summary>
        public virtual Department ParentDepartment
        {
            get { return _parent_department; }
            set { _parent_department = value; }
        }

        /// <summary>
        /// 职员列表。
        /// </summary>
        public virtual IList Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        /// <summary>
        /// 子部门列表。
        /// </summary>
        public virtual IList SubDepartments
        {
            get { return _sub_departments; }
            set { _sub_departments = value; }
        }
				
		#endregion 

        #region 构造函数

        public Department()
		{
			_id = String.Empty;
			_name = String.Empty;
			_phone = String.Empty;
			_ext_number = String.Empty;
			_fax = String.Empty;
			_remark = String.Empty;
			_order_id = 0;
            _parent_department = null;
            _staff = new List<Staff>();
            _sub_departments = new List<Department>();
		}

		#endregion

        #region 公共方法


        /// <summary>
        /// 移动部门。
        /// </summary>
        /// <param name="newParent">新的父部门。</param>
        public virtual void MoveTo(Department newParent)
        {
            if (this.ParentDepartment != null)
            {
                Department oldParent = this.ParentDepartment;
                oldParent.SubDepartments.Remove(this);
            }

            if (newParent != null)
            {
                newParent.SubDepartments.Add(this);
                this.ParentDepartment = newParent;
            }
            else
            {
                this.ParentDepartment = null;
            }
        }


        /// <summary>
        /// 判断当前部门是否包含职员。
        /// </summary>
        /// <returns>true：包含；false：不包含。</returns>
        public virtual bool HasStaff()
        {
            return this.Staff.Count > 0;
        }


        /// <summary>
        /// 从上级部门脱离。（使自己成为顶层的部门）
        /// </summary>
        public virtual void BreakAwayFromParent()
        {
            if (this.ParentDepartment != null)
            {
                Department p = this.ParentDepartment;
                this.ParentDepartment = null;
                p.SubDepartments.Remove(this);
            }
        }


        /// <summary>
        /// 增加一个子部门。
        /// </summary>
        /// <param name="subDepartment">子部门。</param>
        public virtual void AddSubDepartment(Department subDepartment)
        {
            this.SubDepartments.Add(subDepartment);
            subDepartment.ParentDepartment = this;
        }


        /// <summary>
        /// 增加一个职员。
        /// </summary>
        /// <param name="staff">职员。</param>
        public virtual void AddStaff(Staff staff)
        {
            this.Staff.Add(staff);
            staff.Department = this;
        }


        #endregion


        public virtual object Clone()
        {
            return this.MemberwiseClone();
            //throw new NotImplementedException();
        }
    }
}
