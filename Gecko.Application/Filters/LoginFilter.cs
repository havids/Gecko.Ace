using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            //获取当前的Action
            string currentAction = filterContext.RouteData.Values["action"].ToString();
            string currentController = filterContext.RouteData.Values["controller"].ToString();
            //当前action不是 logon 则判断session session为空 跳转到登录页
            if (SessionUtil.GetStaffSession() == null)
            {
                string requestWith = filterContext.HttpContext.Request.Headers["X-Requested-With"];
                //如果为 ajax 请求
                if (!string.IsNullOrEmpty(requestWith) && requestWith.ToLower() == "xmlhttprequest")
                {
                    var contentResult = new ContentResult();
                    contentResult.Content = "登录超时，请刷新页面";
                    filterContext.Result = contentResult;
                }
                else
                {
                    if (currentController.ToLower() == "admin")
                        filterContext.Result = new RedirectResult("/");
                    else
                    {
                        var contentResult = new ContentResult();
                        contentResult.Content = "<script type='text/javascript'>parent.location.href='/Logon/Login';</script>";
                        filterContext.Result = contentResult;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}