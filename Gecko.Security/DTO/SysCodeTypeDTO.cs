using System;
using System.ComponentModel.DataAnnotations;

namespace Gecko.Security.DTO
{
    /// <summary>
    ///	代码分类。
    /// </summary>
    [Serializable]
    public class SysCodeTypeDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "标识不能为空")]
        public string Tag { get; set; }
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "排序ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "排序Id必须大于0")]
        public int OrderId { get; set; }
    }
}
