using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.Models;

namespace Indepp.Controllers
{
    public class UserPlaceController : Controller
    {
        // GET: UserPlace
        public ActionResult CreatePlace(Place? place)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlace(Place place)
        {
            return View();
        }

        public ActionResult PreviewPlace(Place place)
        {
            return View();
        }

        [HttpPost]
        public ActionResult PreviewPlace(Place place)
        {
            return View(); // return a thank you page or thank you text.
        }
    }
}