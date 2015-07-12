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
        public ActionResult Index(int? page)
        {
            ViewBag.PageTitle = "Home";

            var blogPosts = db.BlogPosts.OrderByDescending(pb => pb.ID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(blogPosts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About() 
        {
            ViewBag.PageTitle = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.PageTitle = "Contact";
            ViewBag.MessageSent = false;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string message)
        {
            //ViewBag.PageTitle = "Contact";
            ViewBag.MessageSent = true;
            return View();
        }
    }
}