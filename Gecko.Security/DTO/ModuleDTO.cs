using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Gecko.Security.DTO
{
    /// <summary>
    ///	模块。
    /// </summary>
    public class ModuleDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "模块Tag不能为空")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "模块名称不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "模块地址不能为空")]
        public string ModuleUrl { get; set; }

        public string Remark { get; set; }

        public bool Disabled { get; set; }

        [Required(ErrorMessage = "排序Id不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "排序Id必须大于0")]
        public int OrderId { get; set; }

        public string ModuleTypeId { get; set; }

        public ArrayList ModuleRights { get; set; }

        public ModuleDTO()
        {
            ModuleRights = new ArrayList();
        }
    }
}