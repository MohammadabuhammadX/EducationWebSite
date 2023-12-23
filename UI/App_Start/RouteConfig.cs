using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {               //Order is important as well , so I want to be sure that this will be our first map route
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name:"Home",
                url:"",
                defaults: new {controller ="Home",action="Index"}
                );
            routes.MapRoute(
                name: "Category",
                url: "{CategoryName}",
                defaults: new { controller = "Home", action = "CategoryPostList", CategoryName = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "PostDetial",
                url: "{CategoryName}/{SeoLink}/{ID}",
                defaults: new { controller = "Home", action = "PostDetial", ID = UrlParameter.Optional, CategoryName = UrlParameter.Optional , SeoLink = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
