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
            var routeController = (string)routeData.Values["controller"];

            var returnActive = control == routeController;

            if (routeController == "Home" || routeController == "Admin")
                returnActive = control == routeController && action == routeAction;

            if (routeController == action && routeController == control) // only for admin navigation list item
                returnActive = true;

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
            else if (routeAction == "Login")
                routeController = "admin";
            else if (routeAction == "PlaceMap")
                routeController = "placemap";

            return path + routeController.ToLower() + ".jpg";
        }

        public static string DisplayOpeningHours(this HtmlHelper html, TimeSpan? openTime, TimeSpan? closeTime)
        {
            var open = openTime.HasValue ? openTime.Value.ToString("hh\\:mm") : "";
            var close= closeTime.HasValue ? closeTime.Value.ToString("hh\\:mm") : "";

            return open + " - " + close;
        }

    }
}