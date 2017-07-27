using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Gecko.Security.Domain;
using Gecko.Security.Service;

/// <summary>
/// 由Web层使用，用于判断已登录的职员的授权情况。
/// </summary>
public class PermissionUtil
{
    private PermissionUtil() { }

    #region public static void SaveGrantPermissionsToSession()
    /// <summary>
    /// 在Session中保存当前登录职员对于当前模块拥有的所有肯定授权标示。
    /// <remarks>
    /// 在每一个模块的主界面初始化时被调用，用于在Session中保存当前登录职员对于当前模块拥有的所有肯定授权标示。
    /// 以后模块在每次需要做授权判断时只需依据Session中保存的授权标示判断即可，不用再次读数据库。
    /// 注意：如果是内置职员登录系统，则此函数将不会被调用，同时在以后的任何操作时也不会调用HasGrantPermission函数来做授权判断。
    /// </remarks>
    /// </summary>
    public static void SaveGrantPermissionsToSession()
    {
        StaffSession ss = SessionUtil.GetStaffSession();

        string moduleTag = SessionUtil.GetModuleTag();

        Staff staff = CommonSrv.LoadObjectById(typeof(Staff), ss.LoginId) as Staff;
        Module module = ModuleSrv.GetModuleByTag(moduleTag);

        if (module != null)
        {
            ArrayList alGrantPermissions = staff.GetGrantPermissions(module);
            SessionUtil.SavaGrantPermissions(alGrantPermissions);
        }
    }
    #endregion

    #region public static bool HasGrantPermission(string rightTag)
    /// <summary>
    /// 判断当前已登录职员是否对当前模块的某项权限有肯定的授权。
    /// </summary>
    /// <remarks>
    /// 在每一个模块的主界面加载时被调用，用于确认职员的授权，进而判断哪些操作按钮需要被隐藏。
    /// 在模块的每一项操作（ashx）被执行前再次被调用，用于再次确认职员的授权，防止用户对ashx的恶意调用。
    /// 注意：如果是内置职员登录系统，则不使用Session中保存的授权标示信息做授权判断，而是直接返回true。
    /// </remarks>
    /// <param name="rightTag">权限标示。</param>
    /// <returns>是否有肯定的授权。</returns>
    public static bool HasGrantPermission(string rightTag)
    {
        StaffSession ss = SessionUtil.GetStaffSession();

        if(ss.IsInnerUser == 0)
        {
            ArrayList al = SessionUtil.GetGrantPermissions();
            return al.Contains(rightTag);
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region public static void CheckSecurity(string moduleTag, string rightTag)
    /// <summary>
    /// 检查当前已登录职员是否对当前模块的某项权限有肯定的授权。
    /// </summary>
    /// <remarks>
    /// 在模块的每一项操作（ashx）执行前被调用，用于确认职员的授权，防止用户对ashx的恶意调用。
    /// </remarks>
    /// <param name="moduleTag">模块标示。</param>
    /// <param name="rightTag">权限标示。</param>
    public static void CheckSecurity(string moduleTag, string rightTag)
    {
        if(!SessionUtil.CompareModuleTag(moduleTag)) throw new ModuleSecurityException("模块标示不匹配。");
        if(!PermissionUtil.HasGrantPermission(rightTag)) throw new ModuleSecurityException("无权执行此项操作。");
    }
    #endregion

}
