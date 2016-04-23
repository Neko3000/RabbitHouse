using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RabbitHouse
{
    public static class HtmlHelpers
    {
        public static string IsActive(this HtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            return (controller == routeController && action == routeAction) ? "active" : "";
        }

        public static string IsActiveForController(this HtmlHelper htmlHelper,string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            return controller == routeController ? "active" : "";
        }
        public static string IsActiveForController(this HtmlHelper htmlHelper,params string[] controllers)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var isExistedInControllers = false;
            foreach(var controller in controllers)
            {
                if(controller==routeController)
                {
                    isExistedInControllers = true;
                    break;
                }
            }

            return isExistedInControllers ? "active" : "";
        }
    }
}