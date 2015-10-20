using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.DAL;
using PagedList;
using Indepp.Models;
using System.Data;
using System.Data.Entity;
using Indepp.HelperMethods;
using Indepp.ViewModels;

namespace Indepp.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        public PlaceContext Context;
        private DynamicFilteringMethods DynamicFiltering;

        public AdminController(PlaceContext context, DynamicFilteringMethods dynamicFiltering)
        {
            Context = context;
            DynamicFiltering = dynamicFiltering;
        }

        #region Place Functionality

        public ActionResult Index(string sortOrder, int? page, PlaceFilter filter, PlaceFilter currentPlaceFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (DynamicFiltering.FilterCheck(filter, currentPlaceFilter))
                page = 1;
            else
                filter = currentPlaceFilter;

            ViewBag.CurrentPlaceFilter = filter;

            var places = Context.Places.Where(p => p.Reviewed == true);
            places = DynamicFiltering.FilterPlaces(places, filter); // filter places based on filter
            places = DynamicFiltering.SortPlaces(places, sortOrder); // sort places based on sortOrder

            return View(DynamicFiltering.PlaceList(places, page));
        }

        public ActionResult Create()
        {
            var workingHours = new WorkingHour();
            var place = new Place() { WorkingHours = workingHours.PopulateHours() };
            ViewBag.AvailableCategories = new ViewBagHelperMethods().PopulatePlaceCategories();

            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Place place)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    place.UserContributed = false;
                    place.Reviewed = true;
                    Context.Places.Add(place);
                    Context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.AvailableCategories = new ViewBagHelperMethods().PopulatePlaceCategories();
            return View(place);
        }

        public ActionResult Details(int? id)
        {
            var place = Context.Places.Include("WorkingHours").ToList().Where(p => p.ID == id).FirstOrDefault();

            return View(place);
        }
        
        public ActionResult Edit(int? id)
        {
            var workingHours = new WorkingHour();
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            if (place.WorkingHours.Count() == 0)
                place.WorkingHours = workingHours.PopulateHours().ToList();

            ViewBag.AvailableCategories = new ViewBagHelperMethods().PopulatePlaceCategories();

            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Place place)
        {
            try
            {
                var Inplace = Context.Places.Where(p => p.ID == place.ID).FirstOrDefault();

                Inplace.GooglePlaceId = place.GooglePlaceId;
                Inplace.Name = place.Name;
                Inplace.Category = place.Category;
                Inplace.Website = place.Website;
                Inplace.Telephone = place.Telephone;
                Inplace.Email = place.Email;

                // address update
                if (Inplace.Address == null) {
                    Inplace.Address = new Address() {
                        PlaceID = place.ID,
                        Address1 = place.Address.Address1,
                        Address2 = place.Address.Address2,
                        City = place.Address.City,
                        Country = place.Address.Country,
                        PostCode = place.Address.PostCode,
                        Latitude = place.Address.Latitude,
                        Longitude = place.Address.Longitude
                    };
                }
                else
                {
                    Inplace.Address.Address1 = place.Address.Address1;
                    Inplace.Address.Address2 = place.Address.Address2;
                    Inplace.Address.City = place.Address.City;
                    Inplace.Address.Country = place.Address.Country;
                    Inplace.Address.PostCode = place.Address.PostCode;
                    Inplace.Address.Latitude = place.Address.Latitude;
                    Inplace.Address.Longitude = place.Address.Longitude;
                }

                // place description update
                if (Inplace.PlaceDescription == null)
                    Inplace.PlaceDescription = new PlaceDescription() { Description = place.PlaceDescription.Description };
                else
                    Inplace.PlaceDescription.Description = place.PlaceDescription.Description;

                // working hours update
                if (Inplace.WorkingHours.Count() == 0)
                    Inplace.WorkingHours = place.WorkingHours;
                else
                    foreach (var workingHour in place.WorkingHours)
                    {
                        var dayOfTheWeek = Inplace.WorkingHours.Where(wh => wh.Day == workingHour.Day).SingleOrDefault();
                        dayOfTheWeek.OpenTime = workingHour.OpenTime;
                        dayOfTheWeek.CloseTime = workingHour.CloseTime;
                    }

                if (!Inplace.Reviewed)
                    Inplace.Reviewed = place.Reviewed;

                Context.SaveChanges();

                return RedirectToAction("Details", new { id = place.ID });
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.AvailableCategories = new ViewBagHelperMethods().PopulatePlaceCategories();
            return View("Edit", place);
        }
        
        public ActionResult Delete(int? id)
        {
            var place = Context.Places.Find(id);

            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var place = Context.Places
                .Include("Address")
                .Include("PlaceDescription")
                .Include("WorkingHours")
                .Include("Articles")
                .Where(p => p.ID == id).Single();

            try
            {
                Context.Places.Remove(place);
                Context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete the model");
            }

            return View("Delete", place);
            
        }

        #endregion

        #region BlogPost Functionality

        public ActionResult BlogPostList(string sortOrder, string titleFilter, string shortDescriptionFilter, string titleString, string shortDescriptionString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";
            ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            if (titleString != null || shortDescriptionString != null)
                page = 1;
            else
            {
                titleString = titleFilter;
                shortDescriptionString = shortDescriptionFilter;
            }
                
            ViewBag.TitleFilter = titleString;
            ViewBag.ShortDescriptionFilter = shortDescriptionString;

            var blogPosts = Context.BlogPosts.AsQueryable();

            if (!String.IsNullOrEmpty(titleString))
                blogPosts = blogPosts.Where(bp => bp.Title.Contains(titleString));
            if (!String.IsNullOrEmpty(shortDescriptionString))
                blogPosts = blogPosts.Where(bp => bp.ShortDescription.Contains(shortDescriptionString));

            switch (sortOrder)
            {
                case "title_desc":
                    blogPosts = blogPosts.OrderByDescending(bp => bp.Title);
                    break;
                case "id_desc":
                    blogPosts = blogPosts.OrderByDescending(bp => bp.ID);
                    break;
                case "ID":
                    blogPosts = blogPosts.OrderBy(bp => bp.ID);
                    break;
                case "date_asc":
                    blogPosts = blogPosts.OrderBy(bp => bp.PostedOn);
                    break;
                case "date_desc":
                    blogPosts = blogPosts.OrderByDescending(bp => bp.PostedOn);
                    break;
                default:
                    blogPosts = blogPosts.OrderBy(bp => bp.Title);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(blogPosts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BlogPostCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogPostCreate(BlogPost blogPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blogPost.PostedOn = DateTime.Now;
                    Context.BlogPosts.Add(blogPost);
                    Context.SaveChanges();

                    return RedirectToAction("BlogPostList");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
            }

            return View(blogPost);
        }

        public ActionResult BlogPostDetails(int? id)
        {
            var blogPost = Context.BlogPosts.Find(id);
            return View(blogPost);
        }

        public ActionResult BlogPostEdit(int? id)
        {
            var blogPost = Context.BlogPosts.Find(id);
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogPostEdit(BlogPost blogPost)
        {
            try
            {
                var IndeppBlogPost = Context.BlogPosts.Find(blogPost.ID);

                IndeppBlogPost.Title = blogPost.Title;
                IndeppBlogPost.ShortDescription = blogPost.ShortDescription;
                IndeppBlogPost.Description = blogPost.Description;
                Context.SaveChanges();

                return RedirectToAction("BlogPostDetails", new { id = blogPost.ID });
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("BlogPostEdit", blogPost);
        }

        public ActionResult BlogPostDelete(int? id)
        {
            var blogPost = Context.BlogPosts.Find(id);

            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogPostDelete(int id)
        {
            var blogPost = Context.BlogPosts.Find(id);

            try
            {
                Context.BlogPosts.Remove(blogPost);
                Context.SaveChanges();

                return RedirectToAction("BlogPostList");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete the model");
            }

            return View("BlogPostDelete", blogPost);

        }

        #endregion 

        #region Article Functionality

        public ActionResult ArticleList(string sortOrder, string titleFilter, string mappedToFilter, string titleString, string mappedToString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";
            ViewBag.MappedToSortParam = sortOrder == "mapped_to_asc" ? "mapped_to_desc" : "mapped_to_asc";
            ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            if (titleString != null || mappedToString != null)
                page = 1;
            else
            {
               titleString = titleFilter;
               mappedToString = mappedToFilter;
            }
                

            ViewBag.TitleFilter = titleString;
            ViewBag.MappedToFilter = mappedToString;

            var articles = Context.Articles.AsQueryable();

            if (!String.IsNullOrEmpty(titleString))
                articles = articles.Where(a => a.Title.Contains(titleString));
            if (!String.IsNullOrEmpty(mappedToString))
                articles = articles.Where(a => a.Place.Name.Contains(mappedToString));

            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(bp => bp.Title);
                    break;
                case "id_desc":
                    articles = articles.OrderByDescending(bp => bp.ID);
                    break;
                case "ID":
                    articles = articles.OrderBy(bp => bp.ID);
                    break;
                case "mapped_to_asc":
                    articles = articles.OrderBy(a => a.Place.Name);
                    break;
                case "mapped_to_desc":
                    articles = articles.OrderByDescending(a => a.Place.Name);
                    break;
                case "date_asc":
                    articles = articles.OrderBy(a => a.PostedOn);
                    break;
                case "date_desc":
                    articles = articles.OrderByDescending(a => a.PostedOn);
                    break;
                default:
                    articles = articles.OrderBy(bp => bp.Title);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(articles.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ArticleCreate()
        {
            ViewBag.placeList = new ViewBagHelperMethods().PopulatePlaceDropdown(Context);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleCreate(Article article)
        {
            if (article.PlaceID == 0)
                article.PlaceID = null;

            try
            {
                if (ModelState.IsValid)
                {
                    article.PostedOn = DateTime.Now;
                    Context.Articles.Add(article);
                    Context.SaveChanges();

                    return RedirectToAction("ArticleList");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.placeList = new ViewBagHelperMethods().PopulatePlaceDropdown(Context);

            return View(article);
        }

        public ActionResult ArticleEdit(int? id)
        {
            var article = Context.Articles.Find(id);
            ViewBag.placeList = new ViewBagHelperMethods().PopulatePlaceDropdown(Context);

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleEdit(Article article)
        {
            if (article.PlaceID == 0)
                article.PlaceID = null;

            try
            {
                if (ModelState.IsValid)
                {
                    var IndeppArticle = Context.Articles.Find(article.ID);

                    IndeppArticle.Title = article.Title;
                    IndeppArticle.ShortDescription = article.ShortDescription;
                    IndeppArticle.Description = article.Description;
                    IndeppArticle.ModifiedOn = DateTime.Now;
                    IndeppArticle.PlaceID = article.PlaceID;

                    Context.SaveChanges();

                    return RedirectToAction("ArticleDetails", new { id = article.ID });
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.placeList = new ViewBagHelperMethods().PopulatePlaceDropdown(Context);

            return View("ArticleEdit", article);
        }

        public ActionResult ArticleDetails(int? id)
        {
            var article = Context.Articles.Find(id);

            return View(article);
        }

        public ActionResult ArticleDelete(int? id)
        {
            var article = Context.Articles.Find(id);

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleDelete(int id)
        {
            var article = Context.Articles.Find(id);

            try
            {
                Context.Articles.Remove(article);
                Context.SaveChanges();

                return RedirectToAction("ArticleList");
            }
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to delete the model");
            }

            return View("ArticleDelete", article);
        }

        #endregion

        public ActionResult UserPlaceList(string sortOrder, int? page, PlaceFilter filter, PlaceFilter currentPlaceFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (DynamicFiltering.FilterCheck(filter, currentPlaceFilter))
                page = 1;
            else
                filter = currentPlaceFilter;

            ViewBag.CurrentPlaceFilter = filter;

            var places = Context.Places.Where(p => p.UserContributed == true && p.Reviewed == false);

            places = DynamicFiltering.FilterPlaces(places, filter); // filter places based on filter
            places = DynamicFiltering.SortPlaces(places, sortOrder); // sort places based on sortOrder

            return View(DynamicFiltering.PlaceList(places, page));
        }

        public ActionResult Statistics()
        {
            var places = Context.Places.AsQueryable();
            var articles = Context.Articles.AsQueryable();
            var blogPosts = Context.BlogPosts.AsQueryable();

            var bestContributor = places
                .Where(p => p.UserEmail != null)
                .GroupBy(x => new { x.UserEmail })
                .Select(group => new { Name = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            var topContributors = Context.Places
                .Where(p => p.UserEmail != null)
                .GroupBy(x => new { x.UserEmail, x.UserName })
                .Select(group => new { Email = group.Key.UserEmail, Name = group.Key.UserName, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10);

            var topContributorsList = topContributors.Select(c => new PlaceContributor { UserEmail = c.Email, UserName = c.Name ?? "Anonymous", PlacesContributed = c.Count });

            var statistics = new PlaceStatistics
            {
                CoffeePlaces = places.Where(p => p.Category == "Coffee").Count(),
                FoodPlaces = places.Where(p => p.Category == "Food").Count(),
                FarmPlaces = places.Where(p => p.Category == "Farm").Count(),
                CraftShopPlaces = places.Where(p => p.Category == "CraftShop").Count(),
                FashionPlaces = places.Where(p => p.Category == "Fashion").Count(),
                TotalPlaces = places.Count(),
                UserContributedPlaces = places.Where(p => p.UserContributed == true).Count(),
                ReviewedPlaces = places.Where(p => p.Reviewed == true).Count(),
                TotalArticles = articles.Count(),
                LinkedArticles = articles.Where(a => a.PlaceID != null).Count(),
                TotalBlogPosts = blogPosts.Count(),
                BestContributorEmail = bestContributor != null ? bestContributor.Name.UserEmail + " - " + bestContributor.Count : "",
                TopContributors = topContributorsList
            };

            return View(statistics);
        }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}