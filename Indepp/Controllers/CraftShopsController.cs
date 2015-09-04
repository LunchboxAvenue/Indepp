﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.DAL;
using PagedList;
using Indepp.HelperMethods;
using Indepp.ViewModels;

namespace Indepp.Controllers
{
    public class CraftShopsController : Controller
    {
        private PlaceContext Context;
        private DynamicFilteringMethods DynamicFiltering;

        public CraftShopsController(PlaceContext context, DynamicFilteringMethods dynamicFiltering)
        {
            Context = context;
            DynamicFiltering = dynamicFiltering;
        }

        // GET: CraftShops
        public ActionResult Index(string sortOrder, int? page, PlaceFilter filter, PlaceFilter currentPlaceFilter)
        {
            // sortOrder must be reflected in view
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (filter.Name != null || filter.City != null || filter.Country != null)
                if (filter.Name != currentPlaceFilter.Name || filter.City != currentPlaceFilter.City || filter.Country != currentPlaceFilter.Country)
                    page = 1;
                else
                    filter = currentPlaceFilter;

            ViewBag.CurrentPlaceFilter = filter;

            var places = Context.Places.Where(c => c.Category == "craftShops" && c.Reviewed == true);
            places = DynamicFiltering.FilterPlaces(places, filter); // filter places based on filter
            places = DynamicFiltering.SortPlaces(places, sortOrder); // sort places based on sortOrder

            // setup additional ViewBag items
            ViewBag.PageTitle = "Craft Shops";
            ViewBag.RecentPosts = new ViewBagHelperMethods().GetRecentPosts(Context, 5);

            return View("PlaceList", DynamicFiltering.PlaceList(places, page));
        }

        public ActionResult Details(int? id)
        {
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            return View("PlaceDetails", place);
        }
    }
}