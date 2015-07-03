using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indepp.HelperMethods
{
    public static class HelperMethods
    {
        public static string IsActive(this HtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = control == routeControl && action == routeAction;

            return returnActive ? "active" : "";
        }

        public static string IsActiveBackground(this HtmlHelper html)
        {
            var routeData = html.ViewContext.RouteData;
            var path = "/Content/images/";
            var routeAction = (string)routeData.Values["action"];
            var routeController = (string)routeData.Values["controller"];

            if (routeAction == "About")
                routeController = "aboutus";
            else if (routeAction == "Contact")
                routeController = "contact";

            return path + routeController.ToLower() + ".jpg";
        }

    }
}