using Gecko.Application.Areas.SystemSecurity.Models;
using Gecko.Application.Filters;
using Gecko.Security.Domain;
using Gecko.Security.DTO;
using Gecko.Security.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Areas.SystemSecurity.Controllers
{
    [LoginFilter]
    public class StaffMgrController : Controller
    {
        //
        // GET: /StaffMgr/

        public ActionResult Index()
        {
            return View();
        }

        #region 用户的操作 CRUDM 添加 删除 更新 移动 获取Model

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffCreate(string Id)
        {
            ViewBag.actionStr = "insert";
            return View("StaffInfo", new StaffDTO() { DepartmentId = Id });
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult StaffEdit(string Id)
        {
            ViewBag.actionStr = "update";
            var staffDomain = Gecko.Security.Service.StaffSrv.GetStaffByLoginId(Id);
            StaffDTO dto = new StaffDTO();
            dto.LoginId = staffDomain.LoginId;
            dto.Name = staffDomain.Name;
            dto.Disabled = staffDomain.Disabled;
            dto.OrderId = staffDomain.OrderId;
            dto.IdCard = staffDomain.IdCard;
            dto.Code = staffDomain.Code;
            dto.OfficePhone = staffDomain.OfficePhone;
            dto.ExtNumber = staffDomain.ExtNumber;
            dto.CellPhone = staffDomain.CellPhone;
            dto.FamilyPhone = staffDomain.FamilyPhone;
            dto.Email = staffDomain.Email;
            dto.ZipCode = staffDomain.ZipCode;
            dto.Remark = staffDomain.Remark;
            dto.Address = staffDomain.Address;
            dto.DegreeTag = staffDomain.DegreeTag;
            dto.Sex = staffDomain.Sex;
            dto.PoliticalAppearanceTag = staffDomain.PoliticalAppearanceTag;
            dto.Married = staffDomain.Married;
            dto.Birthday = staffDomain.Birthday;
            dto.CountryTag = staffDomain.CountryTag;
            dto.EntersDay = staffDomain.EntersDay;
            dto.NationTag = staffDomain.NationTag;
            dto.LeavesDay = staffDomain.LeavesDay;
            dto.PositionTag = staffDomain.PositionTag;
            dto.TitleTag = staffDomain.TitleTag;
            return View("StaffInfo", dto);
        }
        [HttpPost]
        public ActionResult StaffInfo(StaffDTO dto, string actionStr)
        {

            //如果为更新 则移除 password 验证
            if (actionStr == "update")
            {
                ModelState.Remove("Password");
            }

            if (ModelState.IsValid)
            {
                //添加
                if (actionStr == "insert")
                {
                    //默认值
                    dto.Birthday = dto.Birthday ?? DateTime.Now;
                    dto.EntersDay = dto.EntersDay ?? DateTime.Now;
                    dto.LeavesDay = dto.LeavesDay ?? DateTime.Now;
                    string result = StaffSrv.InsertStaff(dto);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else if (actionStr == "update")
                {
                    //编辑
                    StaffSrv.UpdateStaff(dto);
                    return Content("1", "text/plain");
                }
            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult StaffDel(string Id)
        {
            string sSucceed = "1";
            try
            {
                StaffSrv.DeleteStaff(Id);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }
        /// <summary>
        /// 移动某个用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StaffMove(string id, string newParentPKId)
        {
            string sSucceed = "1";
            try
            {
                string newDepartmentPKId = newParentPKId;
                StaffSrv.MoveStaff(id, newDepartmentPKId);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }

        public ActionResult GetStaffInfo(string Id)
        {
            var staffDomain = Gecko.Security.Service.StaffSrv.GetStaffByLoginId(Id);
            StaffDTO dto = new StaffDTO();
            dto.LoginId = staffDomain.LoginId;
            dto.Name = staffDomain.Name;
            dto.Disabled = staffDomain.Disabled;
            dto.OrderId = staffDomain.OrderId;
            dto.IdCard = staffDomain.IdCard;
            dto.Code = staffDomain.Code;
            dto.OfficePhone = staffDomain.OfficePhone;
            dto.ExtNumber = staffDomain.ExtNumber;
            dto.CellPhone = staffDomain.CellPhone;
            dto.FamilyPhone = staffDomain.FamilyPhone;
            dto.Email = staffDomain.Email;
            dto.ZipCode = staffDomain.ZipCode;
            dto.Remark = staffDomain.Remark;
            dto.Address = staffDomain.Address;
            dto.DegreeTag = staffDomain.DegreeTag;
            dto.Sex = staffDomain.Sex;
            dto.PoliticalAppearanceTag = staffDomain.PoliticalAppearanceTag;
            dto.Married = staffDomain.Married;
            dto.Birthday = staffDomain.Birthday;
            dto.CountryTag = staffDomain.CountryTag;
            dto.EntersDay = staffDomain.EntersDay;
            dto.NationTag = staffDomain.NationTag;
            dto.LeavesDay = staffDomain.LeavesDay;
            dto.PositionTag = staffDomain.PositionTag;
            dto.TitleTag = staffDomain.TitleTag;

            //将时间格式化成 yyyy-MM-dd 格式
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            string json = JsonConvert.SerializeObject(dto, timeFormat);

            return Content(json, "application/json; charset=utf-8");
        }

        #endregion

        #region 用户Tree
        [HttpGet]
        public ActionResult StaffInfoTree()
        {
            NodeType ntype = new NodeType();
            ntype.id = "0";
            ntype.text = "部门";
            ntype.ntype = "root";
            IList ilDepartMent = DepartmentSrv.GetAllTopDepartment();
            var ilNodeType = GetRoleTypeList(ilDepartMent);
            ntype.children = ilNodeType;
            var rNodeType = new List<NodeType>();
            rNodeType.Add(ntype);
            string jsonResult = JsonConvert.SerializeObject(rNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }

        IList GetRoleTypeList(IList ilRoleType)
        {
            IList l = new List<NodeType>();
            foreach (Department sub in ilRoleType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "department";

                if (sub.SubDepartments.Count > 0)
                {
                    ntype.children = GetRoleTypeList(sub.SubDepartments);
                }

                if (sub.Staff.Count > 0)
                {
                    ntype.children = ntype.children ?? new List<NodeType>();
                    foreach (Staff s in sub.Staff)
                    {
                        NodeType nsubtype = new NodeType();
                        nsubtype.id = s.LoginId;
                        nsubtype.text = s.Name;
                        nsubtype.ntype = "staff";
                        ntype.children.Add(nsubtype);
                    }
                }

                l.Add(ntype);
            }

            return l;
        }

        #endregion

        #region 用户权限模块

        public ActionResult Permissions(string Id)
        {
            ViewBag.loginId = Id;
            return View();
        }
        public ActionResult PermissionsTree(string Id)
        {
            Staff role = CommonSrv.LoadObjectById(typeof(Staff), Id) as Staff;
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
        [HttpPost]
        public ActionResult PermissionsUpdate(string Id, string arrGrantStr)
        {
            string sSucceed = "1";
            try
            {
                //角色ID。
                string loginId = Id;

                //肯定授权。
                string sGrant = arrGrantStr;
                string[] arrGrant = null;
                if (sGrant.Length > 0)
                    arrGrant = sGrant.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    arrGrant = new string[0];

                var arrDeny = new string[0];

                StaffSrv.UpdatePermissions(loginId, arrGrant, arrDeny);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }

        //返回模块分类 模块 序列化List
        IList GetModulePermissionList(IList ilModuleType, SysCodeType sct, Staff staff)
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
                    ntype.children = GetModulePermissionList(sub.SubModuleTypes, sct, staff);
                }

                if (sub.Modules.Count > 0)
                {

                    ntype.children = ntype.children ?? new List<NodeType>();
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
                                rightsType.Checked = staff.ModuleRightsGrant.Contains(mr);
                                l_module_rights.Add(rightsType);
                            }
                        }

                        if (l_module_rights.Count > 0)
                        {
                            nsubtype.children = l_module_rights;
                            ntype.children.Add(nsubtype);
                        }
                    }
                }

                l.Add(ntype);
            }

            return l;
        }

        #endregion

        #region 用户角色模块

        public ActionResult Roles(string Id)
        {
            ViewBag.loginId = Id;
            return View();
        }
        public ActionResult RolesTree(string Id)
        {
            IList ilRoleType = RoleTypeSrv.GetAllTopRoleType();
            Staff staff = CommonSrv.LoadObjectById(typeof(Staff), Id) as Staff;
            NodeType ntype = new NodeType();
            ntype.id = "0";
            ntype.text = "角色分类";
            ntype.ntype = "root";

            var ilNodeType = GetModulePermissionList(ilRoleType, staff);
            ntype.children = ilNodeType;
            var rNodeType = new List<NodeType>();
            rNodeType.Add(ntype);
            string jsonResult = JsonConvert.SerializeObject(rNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }

        //返回模块分类 模块 序列化List
        IList GetModulePermissionList(IList ilModuleType, Staff staff)
        {
            IList l = new List<NodeType>();
            foreach (RoleType sub in ilModuleType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "roletype";
                ntype.state = "closed";

                if (sub.SubRoleTypes.Count > 0)
                {
                    ntype.children = GetModulePermissionList(sub.SubRoleTypes, staff);
                }

                if (sub.Roles.Count > 0)
                {
                    ntype.children = ntype.children ?? new List<NodeType>();
                    foreach (Role r in sub.Roles)
                    {
                        NodeType nsubtype = new NodeType();
                        nsubtype.id = r.Id;
                        nsubtype.text = r.Name;
                        nsubtype.ntype = "role";
                        nsubtype.Checked = staff.Roles.Contains(r);
                        ntype.children.Add(nsubtype);
                    }
                }

                l.Add(ntype);
            }

            return l;
        }

        [HttpPost]
        public ActionResult RolesUpdate(string Id, string sRoleIds)
        {
            string sSucceed = "1";
            try
            {
                //职员ID。
                string StaffId = Id;

                //角色。
                string[] arrRoleIds = null;
                if (sRoleIds.Length > 0)
                    arrRoleIds = sRoleIds.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    arrRoleIds = new string[0];

                StaffSrv.UpdateRoles(StaffId, arrRoleIds);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed);

        }

        #endregion

        #region 用户密码模块

        public ActionResult StaffPassword(string Id)
        {
            ViewBag.loginId = Id;
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassword(string loginId, string pwd)
        {
            string sSucceed = "1";
            try
            {
                string LoginId = loginId;
                string Password = pwd;
                StaffSrv.UpdatePassword(LoginId, Password);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed);

        }

        //修改密码
        public ActionResult ChangePassword(string Id)
        {
            ViewBag.loginId = Id;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string loginId, string oldPwd, string pwd)
        {
            string sSucceed = "-1";
            try
            {
                string LoginId = loginId;
                string Password = pwd;
                if (StaffSrv.UpdatePassword(loginId, oldPwd, Password))
                {
                    sSucceed = "1";
                }
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed);

        }

        #endregion
    }
}
