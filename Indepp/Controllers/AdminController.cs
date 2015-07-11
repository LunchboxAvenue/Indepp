using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.DAL;
using PagedList;
using Indepp.Models;

namespace Indepp.Controllers
{
    public class AdminController : Controller
    {
        public PlaceContext Context;

        public AdminController(PlaceContext context)
        {
            Context = context;
        }

        // GET: Admin
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var places = Context.Places.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                places = places.Where(p => p.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "name_desc":
                    places = places.OrderByDescending(p => p.Name);
                    break;
                case "id_desc":
                    places = places.OrderByDescending(p => p.ID);
                    break;
                case "ID":
                    places = places.OrderBy(p => p.ID);
                    break;
                default:
                    places = places.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(places.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Place place)
        {
            // validate the model here
            Context.Places.Add(place);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            return View(place);
        }
        
        public ActionResult Edit(int? id)
        {
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            return View(place);
        }

        [HttpPost]
        public ActionResult Edit(Place place)
        {
            var Inplace = Context.Places.Where(p => p.ID == place.ID).FirstOrDefault();

            Inplace.Name = place.Name;
            Context.SaveChanges();

            return RedirectToAction("Details", new { id = place.ID });
        }
        
        public ActionResult Delete(int? id)
        {
            var place = Context.Places.Find(id);

            return View(place);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var place = Context.Places.Find(id);
            Context.Places.Remove(place);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}