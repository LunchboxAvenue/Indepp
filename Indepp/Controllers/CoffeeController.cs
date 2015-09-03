using Indepp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Indepp.HelperMethods;

namespace Indepp.Controllers
{
    public class CoffeeController : Controller
    {
        private PlaceContext Context;
        private DynamicFilteringMethods DynamicFiltering;

        public CoffeeController(PlaceContext context, DynamicFilteringMethods dynamicFiltering)
        {
            Context = context;
            DynamicFiltering = dynamicFiltering;
        }

        // GET: Coffee
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.PageTitle = "Coffee";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            ViewBag.RecentPosts = new ViewBagHelperMethods().GetRecentPosts(Context, 5);

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var places = Context.Places.Where(c => c.Category == "coffee" && c.Reviewed == true);

            if (!String.IsNullOrEmpty(searchString))
                places = places.Where(p => p.Name.Contains(searchString));

            places = DynamicFiltering.SortPlaces(places, sortOrder);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View("PlaceList", places.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            return View("PlaceDetails", place);
        }
    }
}