using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;

namespace Gecko.Application.Controllers
{
    public class UrlController : Controller
    {
        //
        // GET: /Url/

        public ActionResult Index()
        {

            string url = Request.QueryString["q"].ToString();
            return Redirect(url);

        }

    }
}
