using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.Models;

namespace Indepp.Controllers
{
    public class ContributeController : Controller
    {
        // GET: UserPlace
        public ActionResult CreatePlace()
        {
            var workingHours = new WorkingHour();
            var place = new Place() { WorkingHours = workingHours.PopulateHours() };
            return View(place);
        }

        [HttpPost]
        public ActionResult PreviewPlace(Place place)
        {
            return View(place);
        }

        [HttpPost]
        public ActionResult SubmitPlace(Place place)
        {
            return View(); // return a thank you page or thank you text.
        }
    }
}