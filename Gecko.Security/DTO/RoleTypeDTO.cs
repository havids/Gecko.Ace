using System;
using System.ComponentModel.DataAnnotations;

namespace Gecko.Security.DTO
{
    /// <summary>
    ///	角色分类。
    /// </summary>
    public class RoleTypeDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "排序Id不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "排序Id必须大于0")]
        public int OrderId { get; set; }
        public string ParentRoleTypeId { get; set; }
    }
}
