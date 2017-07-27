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
    public class ModuleMgrController : Controller
    {
        //
        // GET: /ModuleMgr/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateModule(string Id)
        {
            ModuleDTO dto = new ModuleDTO();
            dto.ModuleTypeId = Id;

            //获取 syscode - rights
            SysCodeType sct = SysCodeTypeSrv.GetSysCodeTypeByTag("rights");
            if (sct != null)
            {
                foreach (SysCode sc in sct.SysCodes)
                {
                    dto.ModuleRights.Add(sc.Name + "|" + sc.Tag + "|" + sc.Id);
                }
            }
            return View("ModuleInfo", dto);
        }

        public ActionResult EditModule(string Id)
        {
            Module mt = CommonSrv.LoadObjectById(typeof(Module), Id) as Module;
            ModuleDTO mdto = new ModuleDTO();
            mdto.Id = mt.Id;
            mdto.ModuleTypeId = mt.ModuleType.Id;
            mdto.ModuleUrl = mt.ModuleUrl;
            mdto.Name = mt.Name;
            mdto.OrderId = mt.OrderId;
            mdto.Remark = mt.Remark;
            mdto.Tag = mt.Tag;
            //获取 syscode - rights
            SysCodeType sct = SysCodeTypeSrv.GetSysCodeTypeByTag("rights");
            if (sct != null)
            {
                foreach (SysCode sc in sct.SysCodes)
                {
                    if (mt.ModuleRights.ContainsKey(sc.Tag))
                        mdto.ModuleRights.Add(sc.Name + "|" + sc.Tag + "|" + sc.Id + "|√");
                    else
                        mdto.ModuleRights.Add(sc.Name + "|" + sc.Tag + "|" + sc.Id);
                }
            }
            return View("ModuleInfo", mdto);
        }

        [HttpPost]
        public ActionResult ModuleInfo(ModuleDTO dto, string[] moduleRights)
        {

            ArrayList myArrayList = new ArrayList();
            ModelState.Remove("ModuleRights");
            var query = from state in ModelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            if (ModelState.IsValid)
            {
                if (moduleRights != null)
                {
                    myArrayList.AddRange(moduleRights);
                    dto.ModuleRights = myArrayList;
                }
                else
                    dto.ModuleRights = new ArrayList();
                //添加
                if (string.IsNullOrEmpty(dto.Id))
                {
                    string result = ModuleSrv.InsertModule(dto);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    //编辑
                    ModuleSrv.UpdateModule(dto);
                    return Content("1", "text/plain");
                }
            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }
        public JsonResult ModuleInfo(string Id)
        {
            JsonResult jresult = new JsonResult();
            jresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                Module mt = CommonSrv.LoadObjectById(typeof(Module), Id) as Module;
                ModuleDTO mdto = new ModuleDTO();
                mdto.Id = mt.Id;
                mdto.ModuleTypeId = mt.ModuleType.Id;
                mdto.ModuleUrl = mt.ModuleUrl;
                mdto.Name = mt.Name;
                mdto.OrderId = mt.OrderId;
                mdto.Remark = mt.Remark;
                mdto.Tag = mt.Tag;
                //获取 syscode - rights
                SysCodeType sct = SysCodeTypeSrv.GetSysCodeTypeByTag("rights");
                if (sct != null)
                {
                    foreach (SysCode sc in sct.SysCodes)
                    {
                        if (mt.ModuleRights.ContainsKey(sc.Tag))
                            mdto.ModuleRights.Add(sc.Name + "|√");
                        else
                            mdto.ModuleRights.Add(sc.Name);
                    }
                }
                jresult.Data = mdto;
            }
            catch
            {
                //TODO js 异常判断
                jresult.Data = "[{result:-1}]";
            }
            return jresult;
        }
        [HttpPost]
        public ActionResult DelModule(string Id)
        {
            string sSucceed = "1";
            try
            {
                ModuleSrv.DeleteModule(Id);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }

            return Content(sSucceed);
        }

        public ActionResult MoveModule()
        {
            return View();
        }
        /// <summary>
        /// 添加moduletype
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        public ActionResult CreateModuleType(string Id)
        {
            return View("ModuleTypeInfo", new ModuleTypeDTO() { ParentModuleTypeId = Id });
        }
        /// <summary>
        /// 编辑moduletype
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EditModuleType(string Id)
        {
            ModuleType mt = CommonSrv.LoadObjectById(typeof(ModuleType), Id) as ModuleType;
            ModuleTypeDTO modultTypeDTO = new ModuleTypeDTO();
            modultTypeDTO.Id = mt.Id;
            modultTypeDTO.Name = mt.Name;
            modultTypeDTO.OrderId = mt.OrderId;
            modultTypeDTO.Remark = mt.Remark;
            modultTypeDTO.ParentModuleTypeId = mt.ParentModuleType == null ? "" : mt.ParentModuleType.Id;
            return View("ModuleTypeInfo", modultTypeDTO);
        }
        [HttpPost]
        public ActionResult ModuleTypeInfo(ModuleTypeDTO dto)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(dto.Id))
                {
                    string result = ModuleTypeSrv.InsertModuleType(dto);
                    if (!string.IsNullOrEmpty(result) && result != "-2")
                        result = "1";
                    return Content(result, "text/plain");
                }
                else
                {
                    ModuleTypeSrv.UpdateModuleType(dto);
                    return Content("1");
                }

            }

            //获取ErrorMessage
            string errorMsg = ModelState.Values.First(x => x.Errors.Count > 0).Errors[0].ErrorMessage;
            return Content(errorMsg, "text/plain");

        }
        /// <summary>
        /// 获取moduletype 详情，返回json 数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult ModuleTypeInfo(string Id)
        {
            JsonResult jresult = new JsonResult();
            jresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                ModuleType mt = CommonSrv.LoadObjectById(typeof(ModuleType), Id) as ModuleType;
                ModuleTypeDTO modultTypeDTO = new ModuleTypeDTO();
                modultTypeDTO.Id = mt.Id;
                modultTypeDTO.Name = mt.Name;
                modultTypeDTO.OrderId = mt.OrderId;
                modultTypeDTO.Remark = mt.Remark;
                jresult.Data = modultTypeDTO;
            }
            catch
            {
                //TODO js 异常判断
                jresult.Data = "[{result:-1}]";
            }
            return jresult;
        }
        /// <summary>
        /// 移动moduletype
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catchall"></param>
        /// <returns></returns>
        public ActionResult MoveModuleType(string id, string catchall)
        {
            string sSucceed = "1";
            try
            {
                ModuleTypeSrv.MoveModuleType(id, catchall);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }
        /// <summary>
        /// 删除moduletype
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DelModuleType(string Id)
        {
            string sSucceed = "1";
            try
            {
                ModuleTypeSrv.DeleteModuleType(Id);
            }
            catch (Exception ex)
            {
                sSucceed = "-1";
            }
            return Content(sSucceed);
        }
        [HttpPost]
        public ActionResult ModuleTypeInfoTree()
        {
            NodeType ntype = new NodeType();
            ntype.id = "0";
            ntype.text = "模块分类";
            ntype.ntype = "root";
            IList ilModuleType = ModuleTypeSrv.GetAllTopModuleType();
            var ilNodeType = GetModuleTypeList(ilModuleType);
            ntype.children = ilNodeType;
            var rNodeType = new List<NodeType>();
            rNodeType.Add(ntype);
            string jsonResult = JsonConvert.SerializeObject(rNodeType, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return Content(jsonResult, "application/json");
        }

        //返回模块分类 模块 序列化List
        IList GetModuleTypeList(IList ilModuleType)
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
                    ntype.children = GetModuleTypeList(sub.SubModuleTypes);
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
                        ntype.children.Add(nsubtype);
                    }
                    //ntype.children = l_module;
                }

                l.Add(ntype);
            }

            return l;
        }

    }
}
