using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gecko.Application.Controllers
{
    public class webhpController : Controller
    {
        //
        // GET: /webhp/

        public ActionResult Index()
        {
            return Redirect("/search");
        }

    }
}
