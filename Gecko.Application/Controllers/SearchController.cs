using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            #region 搜索后返回页面内容

            if (Request.QueryString["q"]!=null && Request.QueryString["q"].ToString()!="")
            {
                var url = string.Format("http://www.google.com/search{0}", Request.Url.Query);
                EasyHttp.Http.HttpClient httpclient = new EasyHttp.Http.HttpClient();
                var response = httpclient.Get(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Content(response.RawText);
                }
                else
                    return Content("error");
            }

            #endregion

            return View();
        }

    }
}
