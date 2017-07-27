using System;
using System.ComponentModel.DataAnnotations;

namespace Gecko.Security.DTO
{
	/// <summary>
	/// 部门。
	/// </summary>
	public class DepartmentDTO
	{

        public DepartmentDTO()
        { }
        

        public string Id{get;set;}
        [Required(ErrorMessage="部门名称不能为空")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ExtNumber { get; set; }
        public string Fax { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "排序Id不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "排序Id必须大于0")]
        public int OrderId { get; set; }
        public string ParentDepartmentId { get; set; }
	}
}
