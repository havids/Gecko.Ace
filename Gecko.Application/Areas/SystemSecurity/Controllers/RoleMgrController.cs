using Gecko.Application.Areas.SystemSecurity.Models;
using Gecko.Application.Filters;
using Gecko.Security.Domain;
using Gecko.Security.DTO;
using Gecko.Security.Service;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Areas.SystemSecurity.Controllers
{
    [LoginFilter]
    public class RoleMgrController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 角色 CRUDM

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="Id">
        /// RoleTypeId
        /// </param>
        /// <returns></returns>
        public ActionResult CreateRole(string Id)
        {
            return View("RoleInfo", new RoleDTO() { RoleTypeId = Id });
        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Id">RoleId</param>
        /// <returns></returns>
        public ActionResult EditRole(string Id)
        {
            Role role = CommonSrv.LoadObjectById(typeof(Role), Id) as Role;
            RoleDTO rdto = new RoleDTO();
            rdto.Id = role.Id;
            rdto.Name = role.Name;
            rdto.OrderId = role.OrderId;
            rdto.Remark = role.Remark;
            rdto.RoleTypeId = role.RoleType != null ? role.RoleType.Id : "";
            return View("RoleInfo", rdto);
        }
        /// <summary>
        /// 添加编辑角色
        /// </summary>
        /// <param name="dto">RoleDTO</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleInfo(RoleDTO dto)
        {
            if (ModelState.IsValid)
            {
                //添加
                if (string.IsNullOrEmpty(dto.Id))
                {
                    string result = RoleSrv.InsertRole(dto);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    //编辑
                    RoleSrv.UpdateRole(dto);
                    return Content("1", "text/plain");
                }
            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");
            
        }
        /// <summary>
        /// 获取角色的详细信息 页面右侧展示信息
        /// </summary>
        /// <param name="Id">RoleId</param>
        /// <returns></returns>
        public JsonResult RoleInfo(string Id)
        {
            JsonResult jresult = new JsonResult();
            jresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                Role role = CommonSrv.LoadObjectById(typeof(Role), Id) as Role;
                RoleDTO rdto = new RoleDTO();
                rdto.Id = role.Id;
                rdto.Name = role.Name;
                rdto.OrderId = role.OrderId;
                rdto.Remark = role.Remark;
                jresult.Data = rdto;
            }
            catch
            {
                //TODO js 异常判断
                jresult.Data = "[{result:-1}]";
            }
            return jresult;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id">RoleId</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelRole(string Id)
        {
            string sSucceed = "1";
            try
            {
                RoleSrv.DeleteRole(Id);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed);
        }
        /// <summary>
        /// 移动角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MoveRole(string id, string newParentPKId)
        {
            string sSucceed = "1";
            try
            {
                RoleSrv.MoveRole(id, newParentPKId);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }

        #endregion

        #region RoleType 角色类型 CRUD

        /// <summary>
        /// 添加Roletype
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        public ActionResult CreateRoleType(string Id)
        {
            return View("RoleTypeInfo", new RoleTypeDTO() { ParentRoleTypeId = Id });
        }
        /// <summary>
        /// 编辑Roletype
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EditRoleType(string Id)
        {
            RoleType roletype = CommonSrv.LoadObjectById(typeof(RoleType), Id) as RoleType;
            RoleTypeDTO dto = new RoleTypeDTO();
            dto.Id = roletype.Id;
            dto.Name = roletype.Name;
            dto.OrderId = roletype.OrderId;
            dto.Remark = roletype.Remark;
            dto.ParentRoleTypeId = roletype.ParentRoleType == null ? "" : roletype.ParentRoleType.Id;
            return View("RoleTypeInfo", dto);
        }
        /// <summary>
        /// 增加 编辑 角色分类
        /// </summary>
        /// <param name="dto">RoleTypeDTO</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleTypeInfo(RoleTypeDTO dto)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(dto.Id))
                {
                    string result = RoleTypeSrv.InsertRoleType(dto);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    RoleTypeSrv.UpdateRoleType(dto);
                    return Content("1");
                }

            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }
        /// <summary>
        /// 获取Roletype 详情，返回json 数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult RoleTypeInfo(string Id)
        {
            JsonResult jresult = new JsonResult();
            jresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                RoleType domain = CommonSrv.LoadObjectById(typeof(RoleType), Id) as RoleType;
                RoleTypeDTO dto = new RoleTypeDTO();
                dto.Id = domain.Id;
                dto.Name = domain.Name;
                dto.OrderId = domain.OrderId;
                dto.Remark = domain.Remark;
                jresult.Data = dto;
            }
            catch
            {
                //TODO js 异常判断
                jresult.Data = "[{result:-1}]";
            }
            return jresult;
        }
        /// <summary>
        /// 移动Roletype
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MoveRoleType(string Id, string newParentPKId)
        {
            string sSucceed = "1";
            try
            {
                RoleTypeSrv.MoveRoleType(Id, newParentPKId);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }
        /// <summary>
        /// 删除Roletype
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DelRoleType(string Id)
        {
            string sSucceed = "1";
            try
            {
                RoleTypeSrv.DeleteModuleType(Id);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 角色赋权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PermissionsTree(string Id)
        {
            Role role = CommonSrv.LoadObjectById(typeof(Role), Id) as Role;
            //权限。
            SysCodeType sct = SysCodeTypeSrv.GetSysCodeTypeByTag("rights");
            NodeType ntype = new NodeType();
            ntype.id = "0";
            ntype.text = "模块分类";
            ntype.ntype = "root";
            IList ilModuleType = ModuleTypeSrv.GetAllTopModuleType();
            var ilNodeType = GetModulePermissionList(ilModuleType, sct, role);
            ntype.children = ilNodeType;
            var rNodeType = new List<NodeType>();
            rNodeType.Add(ntype);
            string jsonResult = JsonConvert.SerializeObject(rNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PermissionsUpdate(string Id, string arrGrantStr)
        {
            string sSucceed = "1";
            try
            {
                //角色ID。
                string RoleId = Id;

                //肯定授权。
                string sGrant = arrGrantStr;
                string[] arrGrant = null;
                if (sGrant.Length > 0)
                    arrGrant = sGrant.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    arrGrant = new string[0];

                var arrDeny = new string[0];

                RoleSrv.UpdatePermissions(RoleId, arrGrant, arrDeny);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }

        public ActionResult Permissions(string Id)
        {
            ViewBag.roleId = Id;
            return View();
        }

        //返回模块分类 模块 序列化List
        IList GetModulePermissionList(IList ilModuleType, SysCodeType sct, Role role)
        {
            IList l = new List<NodeType>();
            foreach (ModuleType sub in ilModuleType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "moduletype";
                ntype.state = "closed";

                if (sub.SubModuleTypes.Count > 0)
                {
                    ntype.children = GetModulePermissionList(sub.SubModuleTypes, sct, role);
                }

                if (sub.Modules.Count > 0)
                {
                    ntype.children = ntype.children ?? new List<NodeType>();
                    //IList l_module = new List<NodeType>();
                    foreach (Module m in sub.Modules)
                    {
                        NodeType nsubtype = new NodeType();
                        nsubtype.id = m.Id;
                        nsubtype.text = m.Name;
                        nsubtype.ntype = "module";

                        IList l_module_rights = new List<NodeType>();
                        //增加肯定权限。
                        foreach (SysCode sc in sct.SysCodes)
                        {
                            if (m.ModuleRights.ContainsKey(sc.Tag))
                            {
                                NodeType rightsType = new NodeType();
                                ModuleRight mr = m.ModuleRights[sc.Tag] as ModuleRight;
                                rightsType.id = mr.Id;
                                rightsType.ntype = "grant";
                                rightsType.text = sc.Name;
                                rightsType.Checked = role.ModuleRightsGrant.Contains(mr);
                                l_module_rights.Add(rightsType);
                            }
                        }

                        if (l_module_rights.Count > 0)
                        {
                            nsubtype.children = l_module_rights;
                            ntype.children.Add(nsubtype);
                        }
                    }
                    //ntype.children = l_module;
                }

                l.Add(ntype);
            }

            return l;
        }

        #endregion

        #region 角色tree


        [HttpPost]
        public ActionResult RoleTypeInfoTree()
        {
            NodeType ntype = new NodeType();
            ntype.id = "0";
            ntype.text = "角色分类";
            ntype.ntype = "root";
            IList ilRoleType = RoleTypeSrv.GetAllTopRoleType();
            var ilNodeType = GetRoleTypeList(ilRoleType);
            ntype.children = ilNodeType;
            var rNodeType = new List<NodeType>();
            rNodeType.Add(ntype);
            string jsonResult = JsonConvert.SerializeObject(rNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }

        //返回模块分类 模块 序列化List
        IList GetRoleTypeList(IList ilRoleType)
        {
            IList l = new List<NodeType>();
            foreach (RoleType sub in ilRoleType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "roletype";

                if (sub.SubRoleTypes.Count > 0)
                {
                    ntype.children = GetRoleTypeList(sub.SubRoleTypes);
                }

                if (sub.Roles.Count > 0)
                {
                    ntype.children = ntype.children ?? new List<NodeType>();
                    foreach (Role m in sub.Roles)
                    {
                        NodeType nsubtype = new NodeType();
                        nsubtype.id = m.Id;
                        nsubtype.text = m.Name;
                        nsubtype.ntype = "role";
                        ntype.children.Add(nsubtype);
                    }
                }

                l.Add(ntype);
            }

            return l;
        }

        #endregion

    }
}
