using Gecko.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

using Gecko.Security.Domain;
using Gecko.Application.Areas.SystemSecurity.Models;

namespace Gecko.Application.Controllers
{
    [LoginFilter]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

                /// <summary>
        /// 获取左侧 Menu 列表
        /// </summary>
        /// <param name="webtagId">
        /// 
        /// </param>
        /// <returns></returns>
        public JsonResult MenuInfo(string Id)
        {
            //获取当前用户
            Staff staff = Gecko.Security.NHHelper.Db.Session.Load(typeof(Staff), SessionUtil.GetStaffSession().LoginId) as Staff;
            //Staff staff = Anole.Security.NHHelper.Db.Session.Load(typeof(Staff),"admin") as Staff;
            IList moduleList = null;
            //if (staff.IsInnerUser == 1)//如果是内置用户 
            //平台如果集成单点登录 则获取当前的平台Id 加载对应的module列表
            //else
            //moduleList = Anole.Security.Service.ModuleTypeSrv.GetTopModuleType("0000000023");
            moduleList = Gecko.Security.Service.ModuleTypeSrv.GetAllTopModuleType();
            var nodeTypeList = GetModuleTypeList(moduleList, staff);
            return Json(nodeTypeList, JsonRequestBehavior.AllowGet);
        }

        //返回模块分类 模块 序列化List
        IList GetModuleTypeList(IList ilModuleType, Staff staff)
        {
            IList l = new List<NodeType>();
            foreach (ModuleType sub in ilModuleType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "moduletype";

                if (sub.SubModuleTypes.Count > 0)
                {
                    ntype.children = GetModuleTypeList(sub.SubModuleTypes, staff);
                }
                
                if (sub.Modules.Count > 0)
                {
                    ntype.children = ntype.children ?? new List<NodeType>();
                    //IList l_module = new List<NodeType>();
                    foreach (Module m in sub.Modules)
                    {
                        ModuleRight mr = Gecko.Security.Service.ModuleRightSrv.GetModuleRight(m, "rights_browse");
                        if (mr != null)
                        {
                            if (staff.HasGrantPermission(mr))
                            {
                                NodeType nsbutype = new NodeType();
                                nsbutype.id = m.Id;
                                nsbutype.text = m.Name;
                                nsbutype.suburl = m.ModuleUrl;
                                ntype.children.Add(nsbutype);
                            }
                        }
                    }
                    //ntype.children = l_module;
                }

                l.Add(ntype);
            }

            return l;
        }
    }
}
