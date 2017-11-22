using Gecko.Security.Domain;
using Gecko.Security.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.ErrorMsg = "请填写用户名密码";
            return View();
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string txtUserName = fc["userName"].ToString();
            string txtPassword = fc["userpwd"].ToString();

            if (string.IsNullOrEmpty(txtUserName) || string.IsNullOrEmpty(txtPassword))
            {
                ViewBag.ErrorMsg = "用户名或密码不能为空";
                return View();
            }

            //验证登录ID和密码。
            Staff s = StaffSrv.GetStaffByLoginIdAndPassword(txtUserName, txtPassword);
            if (s == null)
            {
                ViewBag.ErrorMsg = "用户名或密码错误";
                return View();
            }
            else
            {

                if (s.Disabled == 1)//被禁用。
                {
                    ViewBag.ErrorMsg = "此用户已经被禁用";
                    return View();
                }
            }

            //保存登录信息。
            SessionUtil.SavaStaffSession(new StaffSession(s.LoginId, s.IsInnerUser));
            return Redirect("/Home/Index");
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            SessionUtil.RemoveStaffSession();
            SessionUtil.RemoveGrantPermissions();
            return Redirect("/");
        }

    }
}
