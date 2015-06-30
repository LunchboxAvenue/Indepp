using Indepp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Indepp.Controllers
{
    public class HomeController : Controller
    {
        private PlaceContext db = new PlaceContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Places.ToList());
        }

        public ActionResult Coffee(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var places = db.Places.AsQueryable();

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

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(places.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Restaurants()
        {
            return View();
        }

        public ActionResult Farms()
        {
            return View();
        }

        public ActionResult CraftShops()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult About() 
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.MessageSent = false;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string message)
        {
            ViewBag.MessageSent = true;
            return View();
        }
    }
}