using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indepp.Controllers
{
    public class FoodController : Controller
    {
        // GET: Foods
        public ActionResult Index()
        {
            return View();
        }
    }
}