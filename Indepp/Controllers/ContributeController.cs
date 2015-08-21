using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.Models;
using Indepp.DAL;
using System.Data;

namespace Indepp.Controllers
{
    public class ContributeController : Controller
    {
        public PlaceContext Context;

        public ContributeController(PlaceContext context)
        {
            Context = context;
        }
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
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        public ActionResult SubmitPlace()
        {
            var place = TempData["PlaceConfirmed"] as Place;
            try
            {
                if (ModelState.IsValid)
                {
                    place.UserContributed = true;
                    place.Reviewed = false;
                    Context.Places.Add(place);
                    Context.SaveChanges();

                    return View();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
                ViewBag.Error = "We were unable to save your place, please try again later or contact administrator";
            }

            return View();
        }
    }
}