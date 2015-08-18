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

            if (TempData["PlaceConfirmed"] != null)
                return View(TempData["PlaceConfirmed"] as Place);

            return View(place);
        }

        [HttpPost]
        public ActionResult CreatePlace(Place place)
        {
            if (ModelState.IsValid)
            {
                TempData.Add("Place", place);
                return RedirectToAction("PreviewPlace");
            }

            return View(place);
        }

        [HttpGet]
        public ActionResult PreviewPlace()
        {
            var place = TempData["Place"] as Place;
            TempData.Add("PlaceConfirmed", place);
            return View(place);
        }

        [HttpGet]
        public ActionResult SubmitPlace()
        {
            // save to database
            var place = TempData["PlaceConfirmed"] as Place;
            return View();
        }
    }
}