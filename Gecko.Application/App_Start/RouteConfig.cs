﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gecko.Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "gsearch",
                url: "{controller}",
                defaults: new { action = "index", id = UrlParameter.Optional },
                constraints: new { controller = @"Search|Url|webhp" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Logon", action = "Login", id = UrlParameter.Optional }
            );

        }
    }
}