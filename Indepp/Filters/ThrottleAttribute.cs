using Indepp.DAL;
using Indepp.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Indepp.Filters
{
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public string Message { get; set; }
        public int Seconds { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid)
            {
                var request = filterContext.HttpContext.Request;
                var ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
                bool allowExecute = false;
                string currentController = filterContext.HttpContext.Request.RequestContext.RouteData.GetRequiredString("controller");

                if (HttpRuntime.Cache[ip] == null)
                {
                    HttpRuntime.Cache.Add(ip,
                        true,
                        null,
                        DateTime.Now.AddSeconds(Seconds),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.Low,
                        null);

                    allowExecute = true;
                }

                if (!allowExecute)
                {
                    Message = Message.Replace("{n}", (Seconds/60).ToString());
                    filterContext.Controller.ViewData.ModelState.AddModelError("ExcessiveRequests", Message);

                    if (currentController == "Contribute")
                    {
                        var result = new ViewResult
                        {
                            ViewName = "~/Views/Contribute/CreatePlace.cshtml",
                            ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                            {
                                Model = filterContext.Controller.TempData["PlaceConfirmed"]
                            }
                        };

                        var context = new PlaceContext();

                        result.ViewBag.AvailableCategories = new ViewBagHelperMethods().PopulatePlaceCategories();
                        result.ViewBag.TopContributors = new ViewBagHelperMethods().GetTopContributors(context, 10);
                        result.ViewData.ModelState.AddModelError("ExcessiveRequests", Message);
                        filterContext.Result = result;
                    }
                        
                }
            }

 	        base.OnActionExecuting(filterContext);
        }
    }
}