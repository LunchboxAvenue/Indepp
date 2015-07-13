using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Indepp.DAL;
using PagedList;
using Indepp.Models;
using System.Data;

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
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

            return View(place);
        }
        
        public ActionResult Edit(int? id)
        {
            var place = Context.Places.Where(p => p.ID == id).FirstOrDefault();

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
            var place = Context.Places.Find(id);

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

        #endregion 


        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}