using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatWebClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Auth", action = "login" }
            );
            routes.MapRoute(
                name: "ChatRoom",
                url: "client",
                defaults: new { controller = "chat", action = "Index" }
            );
        }
    }
}
