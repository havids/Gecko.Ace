using Gecko.Security.Domain;
using Gecko.Security.DTO;
using Gecko.Security.Service;
using Gecko.Application.Areas.SystemSecurity.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gecko.Application.Filters;

namespace Gecko.Application.Areas.SystemSecurity.Controllers
{
    [LoginFilter]
    public class DepartMentMgrController : Controller
    {
        //
        // GET: /DepartMent/

        public ActionResult Index()
        {
            return View();
        }

        #region 部门功能模块 CRUDM

        /// <summary>
        /// 部门移动
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepartMentMove(string Id, string newParentPKId)
        {
            string sSucceed = "1";
            try
            {
                newParentPKId = newParentPKId == "0" ? null : newParentPKId;
                DepartmentSrv.MoveDepartment(Id, newParentPKId);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed, "text/plain");

        }

        public ActionResult DepartMentCreate(string Id)
        {
            return View("DepartMentInfo", new Gecko.Security.DTO.DepartmentDTO() { Id = null, ParentDepartmentId = Id });
        }
        public ActionResult DepartMentEdit(string Id)
        {
            var departmentDomain = CommonSrv.LoadObjectById(typeof(Department), Id) as Gecko.Security.Domain.Department;
            DepartmentDTO departmentDTO = new DepartmentDTO();
            departmentDTO.Id = departmentDomain.Id;
            departmentDTO.Name = departmentDomain.Name;
            departmentDTO.OrderId = departmentDomain.OrderId;
            departmentDTO.Phone = departmentDomain.Phone;
            departmentDTO.ExtNumber = departmentDomain.ExtNumber;
            departmentDTO.Fax = departmentDomain.Fax;
            departmentDTO.Remark = departmentDomain.Remark;
            departmentDTO.ParentDepartmentId = departmentDomain.ParentDepartment != null ? departmentDomain.ParentDepartment.Id : null;
            return View("DepartMentInfo", departmentDTO);
        }
        [HttpPost]
        public string DepartMentDel(string Id)
        {
            try
            {
                DepartmentSrv.DeleteDepartment(Id);
            }
            catch (Exception ex)
            {
                return "-1";
            }
            return "1";
        }
        [HttpPost]
        [ActionName("DepartMentInfo")]
        public ActionResult DepartMentInfo(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                //添加
                if (string.IsNullOrEmpty(departmentDTO.Id))
                {
                    string result = DepartmentSrv.InsertDepartment(departmentDTO);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    //编辑
                    DepartmentSrv.UpdateDepartment(departmentDTO);
                    return Content("1", "text/plain");
                }
            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }

        /// <summary>
        /// 获取权限代码Type json 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetDepartMentInfoJson(string Id)
        {
            var departmentDomain = CommonSrv.LoadObjectById(typeof(Department), Id) as Gecko.Security.Domain.Department;
            DepartmentDTO departMentDTO = new DepartmentDTO();
            departMentDTO.Id = departmentDomain.Id;
            departMentDTO.Name = departmentDomain.Name;
            departMentDTO.OrderId = departmentDomain.OrderId;
            departMentDTO.Phone = departmentDomain.Phone;
            departMentDTO.Remark = departmentDomain.Remark;
            departMentDTO.Fax = departmentDomain.Fax;
            departMentDTO.ExtNumber = departmentDomain.ExtNumber;
            return Json(departMentDTO, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 部门Tree

        /// <summary>
        /// 获取部门 tree
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartMentTree()
        {

            IList rootlist = new List<NodeType>();

            var rootNodeType = new NodeType();
            rootNodeType.id = "0";
            rootNodeType.text = "部门管理";
            rootNodeType.state = "open";
            rootNodeType.ntype = "root";

            IList departmentList = DepartmentSrv.GetAllTopDepartment();

            rootNodeType.children = GetDepartmentList(departmentList);

            List<NodeType> rootListNodeType = new List<NodeType>();
            rootListNodeType.Add(rootNodeType);
            string jsonResult = JsonConvert.SerializeObject(rootListNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");

        }
        IList GetDepartmentList(IList ilDepartmentType)
        {
            IList l = new List<NodeType>();
            foreach (Department sub in ilDepartmentType)
            {
                NodeType ntype = new NodeType();
                ntype.id = sub.Id;
                ntype.text = sub.Name;
                ntype.ntype = "department";

                if (sub.SubDepartments.Count > 0)
                {
                    ntype.children = GetDepartmentList(sub.SubDepartments);
                }

                l.Add(ntype);
            }

            return l;
        }

        #endregion

    }
}
