using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Gecko.Security.Domain;

namespace Gecko.Application.Filters
{
    public class ViewFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取当前的Action
            //后台添加模块的标示  需要跟 action 一致
            string currentAction = filterContext.RouteData.Values["action"].ToString();

            //var staff = Gecko.Security.Service.StaffSrv.GetStaffByLoginId(SessionUtil.GetStaffSession().LoginId);
            ////判断用户是否有 当前 action 浏览的权限 暂时不用缓存
            //var m = Gecko.Security.Service.ModuleSrv.GetModuleByTag(currentAction);

            //if (m != null)
            //{
            //    var m_rights = staff.GetGrantPermissions(m);
            //    if (!m_rights.Contains("rights_browse"))
            //    {
            //        var contentResult = new ContentResult();
            //        contentResult.Content = "无权限访问";
            //        filterContext.Result = contentResult;
            //    }
            //}

            //配合 permissionUtil 进行访问
            var isHave = PermissionUtil.HasGrantPermission("rights_browse");
            //如果当前用户没有浏览权限
            if (!isHave)
            {
                var contentResult = new ContentResult();
                contentResult.Content = "无权限访问";
                filterContext.Result = contentResult;
            }

            base.OnActionExecuting(filterContext);

        }
    }
}