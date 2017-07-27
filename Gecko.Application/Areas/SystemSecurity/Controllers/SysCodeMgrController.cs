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
    public class SysCodeMgrController : Controller
    {
        //
        // GET: /SysCodeMgr/
        public ActionResult Index()
        {
            return View();
        }

        #region 权限代码 CRUDM

        public ActionResult SysCodeCreate(string id)
        {
            return View("SysCodeInfo", new Gecko.Security.DTO.SysCodeDTO() { Id = null, SysCodeTypeId = id });
        }
        public ActionResult SysCodeEdit(string Id)
        {
            var sysCode = SysCodeSrv.GetSysCodeByTag(Id);
            SysCodeDTO sysCodeDTO = new SysCodeDTO();
            sysCodeDTO.Id = sysCode.Id;
            sysCodeDTO.Name = sysCode.Name;
            sysCodeDTO.OrderId = sysCode.OrderId;
            sysCodeDTO.Remark = sysCode.Remark;
            sysCodeDTO.SysCodeTypeId = sysCode.SysCodeTypee.Id;
            sysCodeDTO.Tag = sysCode.Tag;
            return View("SysCodeInfo", sysCodeDTO);
        }
        [HttpPost]
        public string SysCodeDel(string Id)
        {
            try
            {
                SysCodeSrv.DeleteSysCode(Id);
            }
            catch (Exception e)
            {
                return "-1";
            }
            return "1";
        }
        [HttpPost]
        public ActionResult SysCodeInfo(SysCodeDTO sysCode)
        {
            if (ModelState.IsValid)
            {
                //添加
                if (string.IsNullOrEmpty(sysCode.Id))
                {
                    string result = SysCodeSrv.InsertSysCode(sysCode);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    //编辑
                    string result = SysCodeSrv.UpdateSysCode(sysCode);
                    return Content(result, "text/plain");
                }
            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }

        #endregion

        #region 权限代码分类 CRUDM
        /// <summary>
        /// 删除当前SysCodeType
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public string SysCodeTypeDel(string Id)
        {
            try
            {
                SysCodeTypeSrv.DeleteSysCodeType(Id);
            }
            catch (Exception e)
            {
                return "-1";
            }
            return "1";
        }
        /// <summary>
        /// 创建 syscodetype
        /// </summary>
        /// <returns></returns>
        public ActionResult SysCodeTypeCreate()
        {
            return View("SysCodeTypeInfo", new SysCodeTypeDTO());
        }
        /// <summary>
        /// 编辑syscodetype
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SysCodeTypeEdit(string Id)
        {
            SysCodeType sct = CommonSrv.LoadObjectById(typeof(SysCodeType), Id) as SysCodeType;
            SysCodeTypeDTO sysCodeTypeDTO = new SysCodeTypeDTO();
            sysCodeTypeDTO.Id = sct.Id;
            sysCodeTypeDTO.Name = sct.Name;
            sysCodeTypeDTO.OrderId = sct.OrderId;
            sysCodeTypeDTO.Remark = sct.Remark;
            sysCodeTypeDTO.Tag = sct.Tag;
            return View("SysCodeTypeInfo", sysCodeTypeDTO);
        }

        [HttpPost]
        public ActionResult SysCodeTypeInfo(SysCodeTypeDTO sysCodeType)
        {
            if (ModelState.IsValid)
            {
                //添加
                if (string.IsNullOrEmpty(sysCodeType.Id))
                {
                    string result = SysCodeTypeSrv.InsertSysCodeType(sysCodeType);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    //编辑
                    string result = SysCodeTypeSrv.UpdateSysCodeType(sysCodeType);
                    return Content(result, "text/plain");
                }
            }
            return View(sysCodeType);
        }

        #endregion

        #region 权限代码Tree
        /// <summary>
        /// 获取 权限代码 tree
        /// </summary>
        /// <returns></returns>
        public ActionResult SysCodeTree(string Id)
        {

            if (!string.IsNullOrEmpty(Id))
                return null;

            IList rootNodeTypeList = new List<NodeType>();

            var rootNodeType = new NodeType();
            rootNodeType.id = "0";
            rootNodeType.text = "权限代码维护";
            rootNodeType.state = "open";
            rootNodeType.ntype = "root";

            IList ilSysCodeType = SysCodeTypeSrv.GetAllSysCodeType();

            foreach (SysCodeType sct in ilSysCodeType)
            {
                var nodeType = new NodeType();
                nodeType.id = sct.Id;
                nodeType.text = sct.Name;
                nodeType.ntype = "sysCodeType";
                if (sct.SysCodes.Count > 0)
                {
                    nodeType.state = "closed";
                    nodeType.children = new List<NodeType>();
                    foreach (SysCode sc in sct.SysCodes)
                    {
                        var nodeSubType = new NodeType();
                        nodeSubType.id = sc.Id;
                        nodeSubType.text = sc.Name;
                        nodeSubType.suburl = sc.Tag;
                        nodeSubType.ntype = "sysCode";
                        nodeSubType.tag = sc.Tag;
                        nodeType.children.Add(nodeSubType);
                    }
                }

                rootNodeTypeList.Add(nodeType);

            }

            rootNodeType.children = rootNodeTypeList;

            List<NodeType> rootListNodeType = new List<NodeType>();
            rootListNodeType.Add(rootNodeType);
            string jsonResult = JsonConvert.SerializeObject(rootListNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }
        /// <summary>
        /// 获取权限代码Type json 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetSysCodeTypeInfoJson(string Id)
        {
            SysCodeType sct = CommonSrv.LoadObjectById(typeof(SysCodeType), Id) as SysCodeType;
            SysCodeTypeDTO sysCodeTypeDTO = new SysCodeTypeDTO();
            sysCodeTypeDTO.Id = sct.Id;
            sysCodeTypeDTO.Name = sct.Name;
            sysCodeTypeDTO.OrderId = sct.OrderId;
            sysCodeTypeDTO.Remark = sct.Remark;
            sysCodeTypeDTO.Tag = sct.Tag;
            return Json(sysCodeTypeDTO, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取权限代码 json
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetSysCodeInfoJson(string Id)
        {
            SysCode sc = CommonSrv.LoadObjectById(typeof(SysCode), Id) as SysCode;
            SysCodeDTO sysDTO = new SysCodeDTO();
            sysDTO.Id = sc.Id;
            sysDTO.Name = sc.Name;
            sysDTO.OrderId = sc.OrderId;
            sysDTO.Remark = sc.Remark;
            sysDTO.Tag = sc.Tag;
            return Json(sysDTO, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
