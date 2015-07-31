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

namespace Indepp.Controllers
{
    public class AdminController : Controller
    {
        public PlaceContext Context;

        public AdminController(PlaceContext context)
        {
            Context = context;
        }

        #region Place Functionality

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
            var workingHours = new WorkingHour();
            var place = new Place() { WorkingHours = workingHours.PopulateHours() };

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
                    Context.Places.Add(place);
                    Context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
            }

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

            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Place place)
        {
            try
            {
                var Inplace = Context.Places.Where(p => p.ID == place.ID).FirstOrDefault();

                Inplace.Name = place.Name;
                Inplace.Category = place.Category;

                // address update
                if (Inplace.Address == null)
                    Inplace.Address = new Address() { PlaceID = place.ID, City = place.Address.City, County = place.Address.County, Country = place.Address.Country };
                else
                {
                    Inplace.Address.City = place.Address.City;
                    Inplace.Address.County = place.Address.County;
                    Inplace.Address.Country = place.Address.Country;
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
                    }

                Context.SaveChanges();

                return RedirectToAction("Details", new { id = place.ID });
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

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

        public ActionResult BlogPostList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var blogPosts = Context.BlogPosts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                blogPosts = blogPosts.Where(bp => bp.Title.Contains(searchString));

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

        public ActionResult ArticleList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.IDSortParam = sortOrder == "ID" ? "id_desc" : "ID";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var articles = Context.Articles.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                articles = articles.Where(a => a.Title.Contains(searchString));

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
            var places = Context.Places.ToList();
            places.Insert(0, new Place() { ID = 0, Name = "None" });
            ViewBag.placeList = places.Select(p => new Place()
            {
                ID = p.ID,
                Name = p.Name
            });

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
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to add a place. Try again, and if the problem persists see your system administrator.");
            }

            return View(article);
        }

        public ActionResult ArticleEdit(int? id)
        {
            var article = Context.Articles.Find(id);

            var places = Context.Places.ToList();
            places.Insert(0, new Place() { ID = 0, Name = "None" });
            ViewBag.placeList = places.Select(p => new Place()
            {
                ID = p.ID,
                Name = p.Name
            });

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


        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}