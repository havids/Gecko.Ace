using Gecko.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

using Gecko.Security.Domain;
using Gecko.Application.Areas.SystemSecurity.Models;

using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Gecko.Application.Controllers
{
    [LoginFilter]
    [ViewFilter]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 欢迎页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
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
        public ActionResult MenuInfo(string Id)
        {
            //获取当前用户
            Staff staff = Gecko.Security.NHHelper.Db.Session.Load(typeof(Staff), SessionUtil.GetStaffSession().LoginId) as Staff;

            List<ModuleType> moduleList = null;

            //适用于两个平台的分类 或者 单独模块的加载
            //在 home index 页面增加 跳转链接
            if (Request.QueryString["moduletype"]==null)
            {
                moduleList = Gecko.Security.Service.ModuleTypeSrv.GetAllTopModuleType().Cast<ModuleType>().ToList();
            }
            else if(Request.QueryString["moduletype"] != null)
            {
                var moduleType = Request.QueryString["moduletype"].ToString();
                var moduleTopType = Gecko.Security.Service.ModuleTypeSrv.GetTopModuleType(moduleType)[0];
                moduleList = ((ModuleType)moduleTopType).SubModuleTypes.Cast<ModuleType>().ToList();
            }
            //获取模块分类
            var nodeTypeList = GetModuleTypeList(moduleList, staff);

            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(nodeTypeList, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                ContentEncoding = Encoding.UTF8
            };
        }

        //返回模块分类 模块 序列化List
        IList GetModuleTypeList(IList ilModuleType, Staff staff)
        {
            IList l = new List<NodeType>();
            foreach (ModuleType sub in ilModuleType)
            {

                //当前 是否对 moduletype 下的 module 有肯定权限
                bool isBrowse = false;

                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "moduletype";
                ntype.tag = sub.Remark;

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
                        //如果禁用 则不显示
                        if (m.Disabled == 1)
                            continue;

                        if (staff.IsInnerUser == 1)//如果是内置用户 平台如果集成单点登录 则获取当前的平台Id 加载对应的module列表
                        {
                            NodeType nsbutype = new NodeType();
                            nsbutype.id = m.Id;
                            nsbutype.text = m.Name;
                            nsbutype.suburl = m.ModuleUrl;
                            ntype.children.Add(nsbutype);
                        }
                        else
                        {

                            try
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

                                        isBrowse = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }

                //查看当前 模块 ntype 是否包含 module
                if (isBrowse || staff.IsInnerUser == 1)
                    l.Add(ntype);

            }

            return l;
        }
    }
}
