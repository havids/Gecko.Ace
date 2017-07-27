using System;
using System.ComponentModel.DataAnnotations;

namespace Gecko.Security.DTO
{
	/// <summary>
	/// 职员。
	/// </summary>
	public class StaffDTO
	{
        [Required(ErrorMessage="登录Id不能为空")]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "登录密码不能为空")]
        public string Password { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }
        public int Sex { get; set; }
        public int Married { get; set; }
        public string IdCard { get; set; }
        public string CountryTag { get; set; }
        public string NationTag { get; set; }
        public string PositionTag { get; set; }
        public string TitleTag { get; set; }
        public string PoliticalAppearanceTag { get; set; }
        public string DegreeTag { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? EntersDay { get; set; }
        public DateTime? LeavesDay { get; set; }
        public string OfficePhone { get; set; }
        public string ExtNumber { get; set; }
        public string FamilyPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Remark { get; set; }
        public int IsInnerUser { get; set; }
        [Required(ErrorMessage = "已禁用不能为空")]
        [Range(0,1)]
        public int Disabled { get; set; }
        [Required(ErrorMessage = "排序Id不能为空")]
        [Range(1,int.MaxValue,ErrorMessage="排序Id必须大于0")]
        public int OrderId { get; set; }
        public string DepartmentId { get; set; }
    }
}
